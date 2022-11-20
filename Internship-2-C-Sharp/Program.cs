using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Internship_2_C_Sharp
{
    public class Program
    {
        #region Initial settings
        public class Group
        {
            public int Points { get; set; }
            public int AchievedPoints { get; set; }
            public int LostPoints { get; set; }
            public int GoalDifference { get; set; }
        }

        public static string gk = "GK";
        public static string df = "DF";
        public static string mf = "MF";
        public static string fw = "FW";

        public static string hrvatska = "Hrvatska";
        public static string maroko = "Maroko";
        public static string belgija = "Belgija";
        public static string kanada = "Kanada";

        public static int matchesPlayed = 0;

        public static Random random = new Random();

        public static Dictionary<string, int> shooters = new Dictionary<string, int>();

        public static Dictionary<string, Group> groupF = new Dictionary<string, Group>()
        {
            { hrvatska, new Group()},
            { belgija, new Group()},
            { kanada, new Group()},
            { maroko, new Group()}
        };

        public static List<string> croatiaResults = new List<string>();

        public static List<string> groupFResults = new List<string>();

        public static Dictionary<string, (string position, int rating)> croatianTeam = new Dictionary<string, (string position, int rating)>()
        {
                { "Dominik Livakovic", (gk,80) },
                { "Ivo Grbic", (gk,74) },
                { "Ivor Pandur", (gk,72) },
                { "Ivica Ivusic", (gk,72) },
                { "Dante Stipica", (gk,71) },
                { "Adrian Semper", (gk,70) },
                { "Ivan Nevistic", (gk,70) },

                { "Luka Modric", (mf,88) },
                { "Marcelo Brozovic", (mf,86) },
                { "Mateo Kovacic", (mf,84) },
                { "Ivan Perisic", (mf,84) },
                { "Ivan Rakitic", (mf,82) },
                { "Mario Pašalic", (mf,81) },
                { "Lovro Majer", (mf,80) },
                { "Josip Brekalo", (mf,79) },

                { "Andrej Kramaric", (fw,82) },
                { "Ante Rebic", (fw,80) },
                { "Marko Livaja", (fw,77) },
                { "Ante Budimir", (fw,76) },
                { "Nikola Kalinic", (fw,74) },
                { "Petar Musa", (fw,74) },
                { "Damir Kreilach", (fw,74) },
                { "Antonio Colak", (fw,73) },

                { "Josko Gvardiol", (df,81) },
                { "Borna Sosa", (df,78) },
                { "Duje Caleta Car", (df,78) },
                { "Dejan Lovren", (df,78) },
                { "Domagoj Vida", (df,76) },
                { "Josip Sutalo", (df,75) },
                { "Josip Juranovic", (df,75) },
                { "Martin Erlic", (df,74) }
        };

        #endregion

        static void Main()
        {
            var options = "\n1 - Odradi trening\n2 - Odigraj utakmicu\n3 - Statistika\n4 - Kontrola igraca\n0 - Izlaz iz aplikacije\n";
            var lowerLimit = 0;
            var upperLimit = 4;
            Selection(Menu(options, "", lowerLimit, upperLimit));
            Console.ReadLine();
        }

        private static int Menu(string options, string optionally, int lowerLimit, int upperLimit)
        {
            Console.WriteLine(optionally);

            Console.WriteLine("\nIzaberite jednu od ponuđenih mogućnosti upisom broja: ");
            bool correctInput;
            int userChoice;
            do
            {
                Console.WriteLine(options);
                correctInput = int.TryParse(Console.ReadLine(), out userChoice);

            } while (correctInput == false || userChoice < lowerLimit || userChoice > upperLimit);

            return userChoice;
        }


        private static void Selection(int choice)
        {
            switch (choice)
            {
                case 0:
                    Exit();
                    break;
                case 1:
                    Training();
                    break;
                case 2:
                    StartMatch();
                    break;
                case 3:
                    var optionally = "\nOdabrali ste opciju : 3 - Statistika.";
                    var options = "\n1 - Ispis svih igrača\n";
                    StatisticsSelection(Menu(options, optionally, 1, 1));
                    break;
                case 4:
                    var optionally4 = "Odabrali ste opciju : \n 4 - Kontrola igrača.";
                    var options4 = "\n1 - Unos novog igrača\n2 - Brisanje igrača\n3 - Uređivanje igrača\n0 - Izlaz iz aplikacije\n";
                    PlayerControlSelection(Menu(options4, optionally4, 0, 4));
                    break;
            }
        }

        private static void PlayerControlSelection(int choice)
        {
            switch (choice)
            {
                case 0:
                    Exit();
                    break;
                case 1:
                    AddNewPlayer();
                    break;
                case 2:
                    var optionally2 = "Odabrali ste opciju : \n 2 - Brisanje igrača.";
                    var options2 = "\n1 - Brisanje igrača unosom imena i prezimena\n";
                    DeletePlayerSelection(Menu(options2, optionally2, 1, 1));
                    //DeletePlayerMenu();
                    break;
                case 3:
                    var optionally3 = "Odabrali ste opciju : \n 3 - Uređivanje igrača.";
                    var options3 = "\n1 - Uredi ime i prezime igrača\n2 - Uredi poziciju igrača\n3 - Uredi rating igrača\n";
                    EditPlayerSelection(Menu(options3, optionally3, 1, 3));
                    //EditPlayerMenu();
                    break;
            }
        }


        private static void EditPlayerSelection(int choice)
        {
            switch (choice)
            {
                case 1:
                    EditPlayerName();
                    break;
                case 2:
                    EditPlayerPosition();
                    break;
                case 3:
                    EditPlayerRating();
                    break;
            }
        }
        private static void DeletePlayerSelection(int choice)
        {
            switch (choice)
            {
                case 1:
                    DeletePlayer();
                    break;
            }
        }

        private static void DeletePlayer()
        {
            Console.WriteLine("Odabrali ste opciju : \n 2 - Brisanje igrača unosom imena i prezimena.");
            Console.WriteLine("Unesite ime i prezime igrača kojeg želite izbrisati : ");

            var player = Console.ReadLine().Trim();

            if (croatianTeam.ContainsKey(player))
            {
                croatianTeam.Remove(player);
                Console.WriteLine($"Igrač {player} izbrisan!");
            }
            else
                Console.WriteLine("Nema igrača sa tim imenom u timu.");
        }

        private static void EditPlayerName()
        {
            Console.WriteLine("Odabrali ste opciju : \n 1 - Uredi ime i prezime igrača.");

            Console.WriteLine("Unesite ime i prezime igrača kojem želite urediti ime : ");

            var player = Console.ReadLine().Trim();
            Console.WriteLine("Unesite novo ime igrača :");
            var newName = Console.ReadLine().Trim();

            if (croatianTeam.ContainsKey(player))
            {
                var value = croatianTeam[player];
                croatianTeam.Remove(player);
                croatianTeam.Add(newName, value);
                Console.WriteLine($"Igrač {player} je sada {newName}!");
            }
            else
                Console.WriteLine("Nema igrača sa tim imenom u timu.");
            Console.WriteLine("Pritisnite bilo koju tipku za povratak u glavni izbornik.");
            Console.ReadKey();
            Main();
        }

        private static void EditPlayerPosition()
        {
            Console.WriteLine("Odabrali ste opciju : \n 2 - Uredi poziciju igrača.");

            Console.WriteLine("Unesite ime i prezime igrača kojem želite urediti poziciju : ");
            var player = Console.ReadLine().Trim();

            Console.WriteLine("Unesite novu poziciju igrača:");
            string newPosition;
            do
            {
                Console.WriteLine("\nUpiši poziciju ( FW / GK / DF / MF )");
                newPosition = Console.ReadLine();
            } while (newPosition != fw && newPosition != gk && newPosition != df && newPosition != mf);

            if (croatianTeam.ContainsKey(player))
            {
                croatianTeam[player] = (newPosition, croatianTeam[player].rating);
                Console.WriteLine($"Pozicija igrača {player} je promijenjena u {newPosition}.");
            }
            else
                Console.WriteLine("Nema igrača sa tim imenom u timu.");

            Console.WriteLine("Pritisnite bilo koju tipku za povratak u glavni izbornik.");
            Console.ReadKey();
            Main();
        }

        private static void EditPlayerRating()
        {
            Console.WriteLine("Odabrali ste opciju : \n 3 - Uredi rating igrača.");

            Console.WriteLine("Unesite ime i prezime igrača kojem želite urediti rating : ");

            var player = Console.ReadLine().Trim();
            Console.WriteLine("Unesite novi rating igrača:");

            bool rating;
            int result;
            do
            {
                rating = int.TryParse(Console.ReadLine(), out result);
            } while (rating == false || result < 1 || result > 100);

            if (croatianTeam.ContainsKey(player))
            {
                croatianTeam[player] = (croatianTeam[player].position, result);
                Console.WriteLine($"Rating igrača {player} je promijenjena u {result}.");
            }
            else
                Console.WriteLine("Nema igrača sa tim imenom u timu.");

            Console.WriteLine("Pritisnite bilo koju tipku za povratak u glavni izbornik.");
            Console.ReadKey();
            Main();
        }

        private static void AddNewPlayer()
        {
            Console.WriteLine("Odabrali ste opciju : \n 1 - Unos novog igrača.");

            if (croatianTeam.Count() == 26)
            {
                Console.WriteLine("Dosegnut je maksimalan broj igrača.");
            }
            else
            {
                Console.WriteLine("Unesite ime i prezime : ");
                var name = Console.ReadLine().Trim();
                if (croatianTeam.ContainsKey(name))
                    Console.WriteLine("Ovaj igrač već postoji u timu. Slijedi povratak u izbornik.");
                else
                {
                    string postionInput;
                    do
                    {
                        Console.WriteLine("\nUpiši poziciju ( FW / GK / DF / MF )");
                        postionInput = Console.ReadLine();
                    } while (postionInput != "FW" && postionInput != "GK" && postionInput != "DF" && postionInput != "MF");
                    var rating = 0;
                    do
                    {
                        Console.WriteLine("Unesite rating za igrača (1-100) :");
                        int.TryParse(Console.ReadLine(), out rating);
                    } while (rating < 1 || rating > 100);

                    croatianTeam.Add(name, (postionInput, rating));
                }
            }

            Console.WriteLine("Pritisnite bilo koju tipku za povratak u glavni izbornik.");
            Console.ReadKey();
            Main();
        }

        private static void StatisticsSelection(int choice)
        {
            switch (choice)
            {
                case 1:
                    PrintAllPlayers();
                    break;
            }
        }
        private static void SecondStatisticsSelection(int choice)
        {
            switch (choice)
            {
                case 1:
                    Console.WriteLine("\nOdabrali ste opciju : 1 - Ispis onako kako su spremljeni\n");
                    foreach (var player in croatianTeam)
                    {
                        Console.WriteLine(player);
                    }
                    Console.WriteLine("Pritisnite bilo koju tipku za povratak u glavni izbornik.");
                    Console.ReadKey();
                    Main();
                    break;
                case 2:
                    Console.WriteLine("\nOdabrali ste opciju : 2 - Ispis po rating uzlazno\n");

                    foreach (var player in croatianTeam.OrderBy(x => x.Value.rating).ToList())
                    {
                        Console.WriteLine(player);
                    }
                    Console.WriteLine("Pritisnite bilo koju tipku za povratak u glavni izbornik.");
                    Console.ReadKey();
                    Main();
                    break;
                case 3:
                    Console.WriteLine("\nOdabrali ste opciju : 3 - Ispis po ratingu silazno\n");
                    foreach (var player in croatianTeam.OrderByDescending(x => x.Value.rating).ToList())
                    {
                        Console.WriteLine(player);
                    }
                    Console.WriteLine("Pritisnite bilo koju tipku za povratak u glavni izbornik.");
                    Console.ReadKey();
                    Main(); break;
                case 4:
                    Console.WriteLine("\nOdabrali ste opciju : 4 - Ispis igrača po imenu i prezimenu(ispis pozicije i ratinga)\n");
                    var input = Console.ReadLine();
                    var searchedPlayer = croatianTeam.FirstOrDefault(x => x.Key == input.Trim());

                    if (searchedPlayer.Key != null)
                        Console.WriteLine(searchedPlayer);
                    else
                        Console.WriteLine("Taj igrač ne postoji.");

                    Console.WriteLine("Pritisnite bilo koju tipku za povratak u glavni izbornik.");
                    Console.ReadKey();
                    Main();
                    break;
                case 5:
                    Console.WriteLine("\nOdabrali ste opciju : 5 - Ispis igrača po ratingu(ako ih je više ispisati sve)\n");

                    var ratingInput = false;
                    int rating;
                    do
                    {
                        ratingInput = int.TryParse(Console.ReadLine(), out rating);
                    } while (!ratingInput);

                    var listOfPlayers = croatianTeam.Where(x => x.Value.rating == rating).ToList();
                    if (listOfPlayers.Count() != 0)
                    {
                        foreach (var player in listOfPlayers)
                        {
                            Console.WriteLine(player);
                        }
                    }
                    else
                        Console.WriteLine("Ne postoji igrač sa traženim ratingom.");

                    Console.WriteLine("Pritisnite bilo koju tipku za povratak u glavni izbornik.");
                    Console.ReadKey();
                    Main();
                    break;
                case 6:
                    Console.WriteLine("\nOdabrali ste opciju : 6 - Ispis igrača po poziciji(ako ih je više ispisati sve)\n");
                    string postionInput;
                    do
                    {
                        Console.WriteLine("\nUpiši poziciju ( FW / GK / DF / MF )");
                        postionInput = Console.ReadLine();
                    } while (postionInput != fw && postionInput != gk && postionInput != df && postionInput != mf);

                    var listofPlayersByPostion = croatianTeam.Where(x => x.Value.position == postionInput).ToList();
                    if (listofPlayersByPostion.Count() != 0)
                    {
                        foreach (var player in listofPlayersByPostion)
                        {
                            Console.WriteLine(player);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Ne postoji igrač sa traženom pozicijom.");
                    }

                    Console.WriteLine("Pritisnite bilo koju tipku za povratak u glavni izbornik.");
                    Console.ReadKey();
                    Main(); break;
                case 7:
                    Console.WriteLine("\nOdabrali ste opciju : 7 - Ispis trenutnih prvih 11 igrača(na pozicijama odabrati igrače s najboljim ratingom\n");
                    var first11 = new List<KeyValuePair<string, (string position, int rating)>>();

                    if (croatianTeam.Where(x => x.Value.position == gk).Count() < 1
                        || croatianTeam.Where(x => x.Value.position == mf).Count() < 4
                        || croatianTeam.Where(x => x.Value.position == df).Count() < 4
                        || croatianTeam.Where(x => x.Value.position == fw).Count() < 3)
                    {
                        Console.WriteLine("Nije moguće pokrenuti utakmicu ako nema dovoljnog broja igrača. ");
                    }
                    else
                        SelectFirst11(first11);
                    Console.WriteLine("Pritisnite bilo koju tipku za povratak u glavni izbornik.");
                    Console.ReadKey();
                    Main();
                    break;
                case 8:
                    Console.WriteLine("\nOdabrali ste opciju : 8 - Ispis strijelaca i koliko golova imaju\n");
                    foreach (var shooter in shooters)
                    {
                        Console.WriteLine(shooter);
                    }
                    Console.WriteLine("Pritisnite bilo koju tipku za povratak u glavni izbornik.");
                    Console.ReadKey();
                    Main();
                    break;
                case 9:
                    Console.WriteLine("\nOdabrali ste opciju : 9 - Ispis svih rezultata ekipe\n");
                    foreach (var result in croatiaResults)
                        Console.WriteLine(result);
                    Console.WriteLine("Pritisnite bilo koju tipku za povratak u glavni izbornik.");
                    Console.ReadKey();
                    Main();
                    break;
                case 10:
                    Console.WriteLine("\nOdabrali ste opciju : 10 - Ispis rezultat svih ekipa\n");
                    foreach (var result in groupFResults)
                        Console.WriteLine(result);
                    Console.WriteLine("Pritisnite bilo koju tipku za povratak u glavni izbornik.");
                    Console.ReadKey();
                    Main();
                    break;
                case 11:
                    Console.WriteLine("\nOdabrali ste opciju : 11 - Ispis tablice grupe(mjesto na tablici, ime ekipe, broj bodova, gol razlika)\n");
                    var order = 1;
                    foreach (var group in groupF.OrderByDescending(x => x.Value.Points).ThenByDescending(x => x.Value.GoalDifference).ToList())
                    {
                        Console.WriteLine($"{order}. {group.Key} {group.Value.Points}  {group.Value.GoalDifference}");
                        order++;
                    }
                    Console.WriteLine("Pritisnite bilo koju tipku za povratak u glavni izbornik.");
                    Console.ReadKey();
                    Main();
                    break;
            }
        }


        private static void StartMatch()
        {
            Console.WriteLine("\nOdabrali ste opciju : 2 - Odigraj utakmicu.\n");

            var first11 = new List<KeyValuePair<string, (string position, int rating)>>();

            if (croatianTeam.Where(x => x.Value.position == gk).Count() < 1
                || croatianTeam.Where(x => x.Value.position == mf).Count() < 4
                || croatianTeam.Where(x => x.Value.position == df).Count() < 4
                || croatianTeam.Where(x => x.Value.position == fw).Count() < 3)
            {
                Console.WriteLine("\nNije moguće pokrenuti utakmicu ako nema dovoljnog broja igrača.\n");
            }
            else
            {
                SelectFirst11(first11);

                var rezultat = (random.Next(0, 3), random.Next(0, 3));
                var rezultat2 = (random.Next(0, 3), random.Next(0, 3));

                matchesPlayed++;

                if (matchesPlayed == 1 || matchesPlayed == 4)
                {
                    if (matchesPlayed == 1)
                    {
                        croatiaResults.Add($"Hrvatska {rezultat.Item1} : {rezultat.Item2} Belgija");
                        groupFResults.Add($"Hrvatska {rezultat.Item1} : {rezultat.Item2} Belgija");
                        groupFResults.Add($"Maroko {rezultat2.Item1} : {rezultat2.Item2} Kanada");
                        Console.WriteLine($"\nRezultat : Hrvatska {rezultat.Item1} : {rezultat.Item2} Belgija");
                    }
                    else
                    {
                        croatiaResults.Add($"Belgija {rezultat.Item2} : {rezultat.Item1} Hrvatska");
                        groupFResults.Add($"Belgija {rezultat.Item2} : {rezultat.Item1} Hrvatska");
                        groupFResults.Add($"Kanada {rezultat2.Item2} : {rezultat2.Item1} Maroko");
                        Console.WriteLine($"\nRezultat : Belgija {rezultat.Item2} : {rezultat.Item1} Hrvatska");
                    }

                    SaveResult(rezultat, hrvatska, belgija);
                    SaveResult(rezultat2, maroko, kanada);

                    RaiseRating(first11, rezultat);
                }
                else if (matchesPlayed == 2 || matchesPlayed == 5)
                {
                    if (matchesPlayed == 2)
                    {
                        croatiaResults.Add($"Hrvatska {rezultat.Item1} : {rezultat.Item2} Kanada");
                        groupFResults.Add($"Hrvatska {rezultat.Item1} : {rezultat.Item2} Kanada");
                        groupFResults.Add($"Maroko {rezultat2.Item1} : {rezultat2.Item2} Belgija");
                        Console.WriteLine($"\nRezultat : Hrvatska {rezultat.Item1} : {rezultat.Item2} Kanada");
                    }
                    else
                    {
                        croatiaResults.Add($"Kanada {rezultat.Item2} : {rezultat.Item1} Hrvatska");
                        groupFResults.Add($"Kanada {rezultat.Item2} : {rezultat.Item1} Hrvatska");
                        groupFResults.Add($"Belgija {rezultat2.Item2} : {rezultat2.Item1} Maroko");
                        Console.WriteLine($"\nRezultat : Kanada {rezultat.Item2} : {rezultat.Item1} Hrvatska\n");
                    }

                    SaveResult(rezultat, hrvatska, kanada);
                    SaveResult(rezultat2, maroko, belgija);

                    RaiseRating(first11, rezultat);
                }
                else if (matchesPlayed == 3 || matchesPlayed == 6)
                {
                    if (matchesPlayed == 1)
                    {
                        croatiaResults.Add($"Hrvatska {rezultat.Item1} : {rezultat.Item2} Maroko");
                        groupFResults.Add($"Hrvatska {rezultat.Item1} : {rezultat.Item2} Maroko");
                        groupFResults.Add($"Kanada {rezultat2.Item1} : {rezultat2.Item2} Belgija");
                        Console.WriteLine($"\nRezultat : Hrvatska {rezultat.Item1} : {rezultat.Item2} Maroko");
                    }
                    else
                    {
                        croatiaResults.Add($"Maroko {rezultat.Item2} : {rezultat.Item1} Hrvatska");
                        groupFResults.Add($"Maroko {rezultat.Item2} : {rezultat.Item1} Hrvatska");
                        groupFResults.Add($"Belgija {rezultat2.Item2} : {rezultat2.Item1} Kanada");
                        Console.WriteLine($"\nRezultat : Maroko {rezultat.Item2} : {rezultat.Item1} Hrvatska");
                    }

                    SaveResult(rezultat, hrvatska, maroko);
                    SaveResult(rezultat2, kanada, belgija);

                    RaiseRating(first11, rezultat);
                }
                else
                {
                    Console.WriteLine("\nOdigrane su sve utakmice.");
                }
            }

            Console.WriteLine("\nRezultati skupine : ");
            foreach (var team in groupF)
            {
                Console.WriteLine($"{team.Key}  {team.Value.Points}");
            }

            Console.WriteLine("\nPritisnite bilo koju tipku za povratak u izbornik.");
            Console.ReadLine();

            Main();
        }

        private static void SaveResult((int, int) result, string firstTeam, string secondTeam)
        {
            if (result.Item1 == result.Item2)
            {
                groupF[firstTeam].Points += 1;
                groupF[secondTeam].Points += 1;
            }
            else if (result.Item1 > result.Item2)
            {
                groupF[firstTeam].Points += 3;
            }
            else
            {
                groupF[secondTeam].Points += 3;
            }

            groupF[firstTeam].AchievedPoints += result.Item1;
            groupF[firstTeam].LostPoints += result.Item2;
            groupF[firstTeam].GoalDifference = groupF[firstTeam].AchievedPoints - groupF[firstTeam].LostPoints;

            groupF[secondTeam].AchievedPoints += result.Item2;
            groupF[secondTeam].LostPoints += result.Item1;
            groupF[secondTeam].GoalDifference = groupF[secondTeam].AchievedPoints - groupF[secondTeam].LostPoints;
        }

        private static void SelectFirst11(List<KeyValuePair<string, (string position, int rating)>> first11)
        {
            var gKs = croatianTeam.Where(x => x.Value.position == gk).OrderByDescending(x => x.Value.rating).ToList();

            var mFs = croatianTeam.Where(x => x.Value.position == mf).OrderByDescending(x => x.Value.rating).ToList().Take(4);

            var fWs = croatianTeam.Where(x => x.Value.position == fw).OrderByDescending(x => x.Value.rating).ToList().Take(3);

            var dFs = croatianTeam.Where(x => x.Value.position == df).OrderByDescending(x => x.Value.rating).ToList().Take(4);

            first11.Add(gKs.First());
            first11.AddRange(mFs.Take(4));
            first11.AddRange(dFs.Take(4));
            first11.AddRange(fWs.Take(4));

            Console.WriteLine("\nPrvih 11 :");
            foreach (var player in first11)
            {
                Console.WriteLine(player);
            }
        }

        private static void RaiseRating(List<KeyValuePair<string, (string position, int rating)>> first11, (int, int) result)
        {
            var numberOfShooters = shooters.Count();
            if (result.Item1 > 0)
            {
                for (int i = 0; i < result.Item1; i++)
                {
                    var igrac = first11[random.Next(0, 12)];
                    if (!shooters.Any(x => x.Key == igrac.Key))
                        shooters.Add(igrac.Key, 1);
                    else
                        shooters[igrac.Key] += 1;
                }

                for (int i = 0; i < 11; i++)
                {
                    if (shooters.Any(x => x.Key == first11[i].Key))
                    {
                        croatianTeam[first11[i].Key] = (croatianTeam[first11[i].Key].position, (int)Math.Round(first11[i].Value.rating + first11[i].Value.rating * 0.05));
                    }
                    else
                    {
                        if (result.Item1 > result.Item2)
                            croatianTeam[first11[i].Key] = (croatianTeam[first11[i].Key].position, (int)Math.Round(first11[i].Value.rating + first11[i].Value.rating * 0.02));
                        else if (result.Item1 < result.Item2)
                            croatianTeam[first11[i].Key] = (croatianTeam[first11[i].Key].position, (int)Math.Round(first11[i].Value.rating - first11[i].Value.rating * 0.02));
                    }
                }
            }

            Console.WriteLine("\nStrijelci : ");
            for (int i = numberOfShooters; i < shooters.Count(); i++)
                Console.WriteLine(shooters.ElementAt(i));
        }

        private static void Training()
        {
            Console.WriteLine("\nOdabrali ste opciju : 1 - Odradi trening.\n");
            var random = new Random();

            foreach (var player in croatianTeam.ToList())
            {
                Console.Write($"\nIgrac {player.Key} igra poziciju {croatianTeam[player.Key].position} i ima rating {croatianTeam[player.Key].rating} prije treninga, ");
                double divisor = 100;
                var newRating = player.Value.rating + (int)(Math.Round(player.Value.rating * (random.Next(0, 6) / divisor)));
                croatianTeam[player.Key] = (player.Value.position, newRating);
                Console.WriteLine($"a {croatianTeam[player.Key].rating} poslije treninga.\n");
            }

            Console.WriteLine("Pritisnite bilo koju tipku za povratak u izbornik.");
            Console.ReadLine();

            Main();
        }

        private static void Exit()
        {
            Console.WriteLine("\ndabrali ste opciju : 0 - Izlaz iz aplikacije.\n");
            var closeApp = "";
            do
            {
                Console.WriteLine("Jeste li sigurni da želite izaći iz aplikacije? (DA/NE)");
                closeApp = Console.ReadLine().Trim();

            } while (closeApp != "DA" && closeApp != "NE");
            if (closeApp == "DA")
                Environment.Exit(0);
            else
                Main();
        }

        private static void PrintAllPlayers()
        {
            var correctInput = false;
            var choice = 0;
            do
            {
                Console.WriteLine("1 - Ispis onako kako su spremljeni\n" +
                    "2 - Ispis po rating uzlazno\n" +
                    "3 - Ispis po ratingu silazno\n" +
                    "4 - Ispis igrača po imenu i prezimenu(ispis pozicije i ratinga)\n" +
                    "5 - Ispis igrača po ratingu(ako ih je više ispisati sve)\n" +
                    "6 - Ispis igrača po poziciji(ako ih je više ispisati sve)\n" +
                    "7 - Ispis trenutnih prvih 11 igrača(na pozicijama odabrati igrače s najboljim ratingom\n" +
                    "8 - Ispis strijelaca i koliko golova imaju\n" +
                    "9 - Ispis svih rezultata ekipe\n" +
                    "10 - Ispis rezultat svih ekipa\n" +
                    "11 - Ispis tablice grupe(mjesto na tablici, ime ekipe, broj bodova, gol razlika)\n");
                correctInput = int.TryParse(Console.ReadLine(), out choice);

            } while (correctInput == false || choice < 1 || choice > 12);

            SecondStatisticsSelection(choice);
        }


    }
}
