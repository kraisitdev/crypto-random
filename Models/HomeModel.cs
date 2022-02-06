namespace crypto_random.Models;

public class HomeModel {

    public class Player {
        public string? PlayerName { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? Modifydate { get; set; }
    }

    public class DataPlayer {
        public string? PlayerName { get; set; }
        public List<Transaction> LsTrans { get; set; }
    }

    public class Transaction {
        public string? CoinCode { get; set; }
        public string? CoinName { get; set; }
        public int? CoinUnit { get; set; }
        public DateTime? Modifydate { get; set; }
    }

    public class ResponseBitKubSymbols {
        public int error { get; set; }
        public List<Result> result { get; set; }
    }

    public class Result {
        public int id { get; set; }
        public string symbol { get; set; }
        public string info { get; set; }
    }
}