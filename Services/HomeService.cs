using System.Text.Json;
using crypto_random.DataAccesses.CryptoRandomDbContext;
using crypto_random.DataAccesses.CryptoRandomDbContext.Context;
using static crypto_random.Models.HomeModel;

namespace crypto_random.Services;

public class HomeService {
    private readonly IConfiguration _conf;
    private readonly AppDb _appDb;

    public HomeService(IConfiguration conf, AppDb appDb) {
        this._conf = conf;
        this._appDb = appDb;
    }

    private async Task<ResponseBitKubSymbols> GetBitKubSymbols() {
        var bitKubUrl = this._conf.GetValue<String>("BitKub:Url");
        var symbolsPath = "/api/market/symbols";

        var httpClientHandler = new HttpClientHandler();
        httpClientHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;

        var httpClient = new HttpClient(httpClientHandler);
        var res = await httpClient.GetAsync(bitKubUrl + symbolsPath);
        var content = await res.Content.ReadAsStringAsync();
        var resMsgBitKubSymbols = JsonSerializer.Deserialize<ResponseBitKubSymbols>(content);

        return resMsgBitKubSymbols;
    }

    public string RandomCoin(string player) {
        var resSymbols = this.GetBitKubSymbols().GetAwaiter().GetResult();
        var random = new Random();
        var ix = random.Next(0, resSymbols.result.Count());
        var unit = random.Next(0, 10);
        var currSymbols = resSymbols.result[ix];

        this._appDb.TblTranactions.Add(new TblTranaction
        {
            CoinCode = currSymbols.symbol,
            CoinName = currSymbols.info,
            CoinUnit = unit,
            PlayerName = player,
            Modifydate = DateTime.Now,
        });
        this._appDb.SaveChanges();

        return $"คุณได้รับเหรียญ {currSymbols.symbol} ({currSymbols.info}) จำนวน {unit.ToString()} เหรียญ";
    }

    public List<Player> GetListPlayer() {
        var lsPlayer = this._appDb?.TblPlayers
                        .Where(s => s.IsActive == true)
                        .Select(s => new Player
                        {
                            PlayerName = s.PlayerName,
                            IsActive = s.IsActive,
                            Modifydate = s.Modifydate,
                        })
                        .ToList();

        return lsPlayer;
    }

    public DataPlayer GetDataPlayer(string playerName) {
        var dataPlayer = this._appDb?.TblPlayers
                .Where(s => s.PlayerName == playerName && s.IsActive == true)
                .Select(s => new DataPlayer
                {
                    PlayerName = s.PlayerName,
                    LsTrans = s.TblTranactions.Select(s => new Transaction
                    {
                        CoinCode = s.CoinCode,
                        CoinName = s.CoinName,
                        CoinUnit = s.CoinUnit,
                        Modifydate = s.Modifydate
                    }).OrderByDescending(s => s.Modifydate).ToList(),
                })
                .FirstOrDefault();

        return dataPlayer;
    }
}