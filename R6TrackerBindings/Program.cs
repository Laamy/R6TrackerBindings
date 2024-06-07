using System;

public class Program
{
    static void Main(string[] args)
    {
        SixBinds r6 = new SixBinds();

        r6.OnEvent += OnGameEvent;
        r6.StartWatching();

        Console.ReadKey(); // press key to escape
    }

    private static void OnGameEvent(object sender, NetPacket R6_Pkt)
    {
        Console.WriteLine(R6_Pkt["feature"]);
        if (R6_Pkt["feature"] == "match_info")
        {
            var info = R6_Pkt["info"];
            var main = new NetPacket(info[R6_Pkt["feature"]]);

            //var moveLog = SixBinds.ParsePkt(main["move_log"]);

            Console.WriteLine(main["map_id"]);
            Console.WriteLine(main["game_mode"]);
            Console.WriteLine(main["match_id"]);
            Console.WriteLine(main["kill_log"]);
            Console.WriteLine(main["ko_log"]);
            Console.WriteLine(main["pseudo_match_id"]);
            Console.WriteLine(main["round_outcome_type"]);
            Console.WriteLine(main["general_log"]);
            Console.WriteLine(main["match_start_log"]);
            Console.WriteLine(main["round_start_log"]);
            Console.WriteLine(main["round_end_log"]);
            Console.WriteLine(main["move_log"]);
            Console.WriteLine(main["score_log"]);
            Console.WriteLine(main["kideath_logll_log"]);
            Console.WriteLine(main["match_end_log"]);

            //Console.WriteLine($"Role: {moveLog["ROLE"]}, RoleID: {moveLog["TEAM"]}");            //Console.WriteLine($"Posture: {moveLog["POSTURE"]}, Type: {moveLog["MOVETYPE"]}");
            //Console.WriteLine($"Position {moveLog["X"]}, {moveLog["Y"]}, {moveLog["Z"]}");
            //Console.WriteLine($"Map {moveLog["MAP"]}");
            //Console.WriteLine($"LocalPlayer {moveLog["CLASS"]}");
            //Console.WriteLine($"Weapon {moveLog["WEAPON"]}");
            //Console.WriteLine($"CTU {moveLog["CTU"]}");
            //Console.WriteLine();
        }
        else
        {
            //Console.WriteLine(R6_Pkt["feature"]);
        }
    }
}