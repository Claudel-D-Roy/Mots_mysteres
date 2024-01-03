using System;
using System.Collections.Generic;
using System.IO;
namespace Tp4
{
    /// <summary>
    /// Auteur :            Claudel D. Roy
    /// Description :       Un jeux de mot mystère 
    /// Date:               2021-12-13
    /// </summary>
    class Program
    {
        static char[,] _acGrille = null;
        static bool[,] _abGrille = null;
        static string[] _arLigne = null;
        static List<string> _listMots = new List<string>();
        static List<bool> _listbCouleur = new List<bool>();
        static List<int> _listTrouver = new List<int>();
        static StreamReader _sGrille = null;
        static string _sReponse = "amende";
        static string _sRepMot = "";
        static string _sRepMot2 = "";
        static int _iNumMot = 0;
        static int _iNum1 = 0;
        static void Main(string[] args)
        {


            DemandeFichier();
            AfficherGrille();
            AfficherMots();
            TrouverMot();
            MotGrille();
        }

        /// <summary>
        /// Permet d'initialiser un ficher de départ. 
        /// </summary>
        private static void DemandeFichier()
        {
            string sReponse = "";

            do
            {
                Console.Write("Veuillez entrer le nom de la grille : ");
                sReponse = Console.ReadLine();
                Console.Clear();

                if (!File.Exists(sReponse))
                {
                    Console.WriteLine("Le fichier n'existe pas!");

                }
                else
                    _sGrille = new StreamReader(sReponse);

            } while (!File.Exists(sReponse));



        }
        /// <summary>
        /// Permet d'afficher la grille du mot caché. 
        /// </summary>
        private static void AfficherGrille()
        {
            int iColonnetab = 0;
            int iLignetab = 0;
            string sLigne = "";
            int iLongeur = 0;



            sLigne = _sGrille.ReadLine();
            _arLigne = sLigne.Split("  ");
            iLongeur = _arLigne.Length;
            _acGrille = new char[iLongeur, iLongeur];
            _abGrille = new bool[iLongeur, iLongeur];



            for (int i = 0; i < iLongeur; i++)
            {
                for (int j = 0; j < iLongeur; j++)
                    _acGrille[i, j] = char.Parse(_arLigne[j]);

                sLigne = _sGrille.ReadLine();
                _arLigne = sLigne.Split("  ");

            }
            Console.WriteLine("     ***********************MOT-MYSTÈRE*********************** ");
            for (int i = 0; i < iLongeur; i++)
            {
                if (iColonnetab == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("    " + iColonnetab.ToString("00") + " ");
                    Console.ResetColor();
                }
                else if (iColonnetab > 0)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(iColonnetab.ToString("00") + " ");
                    Console.ResetColor();
                }
                iColonnetab++;
            }

            Console.WriteLine();
            for (int i = 0; i < iLongeur; i++)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(iLignetab.ToString("00") + "   ");
                Console.ResetColor();

                for (int j = 0; j < iLongeur; j++)
                {
                    Console.Write(_acGrille[i, j] + "  ");
                    _abGrille[i, j] = true;
                }

                iLignetab++;
                Console.WriteLine();
            }


        }
        /// <summary>
        /// Affiche la liste des mots a trouver.
        /// </summary>
        private static void AfficherMots()
        {
            int iCompteMot = 1;
            int iCompteMot2 = 0;
            int iNumeroDeMot = 0;
            int iNumeroDeMot2 = 0;
            int iNumeroDeMot3 = 0;
            string sMot = "";

            Console.WriteLine();
            Console.WriteLine("*****************************************");
            Console.WriteLine("Liste de mots à trouver (" + _iNum1 + " à 68) : ");
            Console.WriteLine("*****************************************");



            for (int i = 0; i < 68; i++)
            {

                sMot = _sGrille.ReadLine();
                if (sMot.Contains("*"))
                {
                    i--;
                }
                else if (string.IsNullOrWhiteSpace(sMot))
                {
                    i--;
                }
                else
                    _listMots.Add(sMot);

                _listbCouleur.Add(true);
            }

            for (int i = 0; i < 10; i++)
            {


                iNumeroDeMot2 = 0;
                iNumeroDeMot3 = 0;
                iCompteMot2 = 0;
                iCompteMot2 = iCompteMot + iCompteMot2;
                if (iCompteMot2 == 10)
                {
                    Console.Write("\n" + iCompteMot + ") " + _listMots[iNumeroDeMot].PadRight(19, ' '));
                }
                else
                    Console.Write("\n" + iCompteMot + ") " + _listMots[iNumeroDeMot].PadRight(20, ' '));

                for (int j = 0; j < 6; j++)
                {
                    iCompteMot2 = iCompteMot2 + 10;
                    iNumeroDeMot2 = iNumeroDeMot2 + 10;
                    iNumeroDeMot3 = iNumeroDeMot + iNumeroDeMot2;

                    if (iNumeroDeMot3 > 68 || iCompteMot2 > 68)
                    {
                        break;
                    }
                    if (iCompteMot2 == 20 && iCompteMot2 == 30 && iCompteMot2 == 40 && iCompteMot2 == 50 && iCompteMot2 == 60)
                    {
                        Console.Write(iCompteMot2 + ") " + _listMots[iNumeroDeMot3].PadRight(19, ' '));
                    }
                    else
                        Console.Write(iCompteMot2 + ") " + _listMots[iNumeroDeMot3].PadRight(20, ' '));
                }

                iCompteMot++;
                iNumeroDeMot++;

            }
        }
        /// <summary>
        /// Trouve le mot dans la grille et confirme la réponse final.
        /// </summary>
        private static void TrouverMot()
        {
            string sReponse = "";
            char cRep = ' ';
            int iVerifieChiffre = 0;
            bool bVerifie = true;


            Console.WriteLine();

            do
            {

                Console.Write("Entrer le numéro du mot : ");
                sReponse = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(sReponse.ToUpper()))
                {
                    Console.WriteLine("Veuillez entrer une réponse valide.");
                }


                for (int i = 0; i < sReponse.Length; i++)
                {
                    cRep = sReponse[i];
                    if (char.IsLetter(cRep))
                    {
                        if (sReponse.ToUpper() == _sReponse.ToUpper())
                        {
                            Console.WriteLine("Bravo!");
                            break;
                        }
                        else if (sReponse.ToUpper() != _sReponse.ToUpper())
                        {
                            Console.WriteLine("ERREUR: Vous n'avez pas trouvé le bon mot");
                            break;
                        }

                    }
                    if (char.IsNumber(cRep))
                    {
                        _iNumMot = int.Parse(sReponse);
                        if (_iNumMot > 0 && _iNumMot < 69)
                        {
                            do
                            {
                                Console.Write("Entrer la coordonnée de la première lettre du mot " + _listMots[_iNumMot - 1] + ":");
                                _sRepMot = Console.ReadLine();


                                iVerifieChiffre = int.Parse(_sRepMot[0].ToString() + _sRepMot[1].ToString());
                                if (iVerifieChiffre > _acGrille.GetLength(0))
                                {
                                    Console.WriteLine("Veuillez entrer une coordonnée valide!");
                                    bVerifie = false;

                                }
                                else
                                    bVerifie = true;


                                iVerifieChiffre = int.Parse(_sRepMot[3].ToString() + _sRepMot[4].ToString());
                                if (iVerifieChiffre > _acGrille.GetLength(0))
                                {
                                    Console.WriteLine("Veuillez entrer une coordonnée valide!");
                                    bVerifie = false;

                                }
                                else
                                    bVerifie = true;


                            } while (bVerifie != true);

                            do
                            {

                                Console.Write("Entrer la coordonnée de la dernière lettre du mot " + _listMots[_iNumMot - 1] + ":");
                                _sRepMot2 = Console.ReadLine();

                                iVerifieChiffre = int.Parse(_sRepMot2[0].ToString() + _sRepMot2[1].ToString());
                                if (iVerifieChiffre > _acGrille.GetLength(0))
                                {
                                    Console.WriteLine("Veuillez entrer une coordonnée valide!");
                                    bVerifie = false;

                                }
                                else
                                    bVerifie = true;


                                iVerifieChiffre = int.Parse(_sRepMot2[3].ToString() + _sRepMot2[4].ToString());
                                if (iVerifieChiffre > _acGrille.GetLength(0))
                                {
                                    Console.WriteLine("Veuillez entrer une coordonnée valide!");
                                    bVerifie = false;

                                }
                                else
                                    bVerifie = true;


                            } while (bVerifie != true);

                            break;

                        }
                        else if (_iNumMot > 68)
                        {
                            bVerifie = false;
                            Console.WriteLine("ERREUR: le numéro du mot ne correspond pas!");
                            break;
                        }
                    }

                }

            } while (sReponse.ToUpper() != _sReponse.ToUpper() && bVerifie != true);

        }
        /// <summary>
        /// Trouve les mots dans la grille.
        /// </summary>
        private static void MotGrille()
        {
            string sReponse = "";
            char cRep = ' ';
            int iVerifieChiffre = 0;
            bool bVerifie = true;
            int iAxe = 0;
            int iAYe = 1;
            int iNumMot = 0;
            int iCoordo = 0;
            int iCoordo2 = 0;
            int iCoordo3 = 0;
            int iCoordo4 = 0;
            int iLignetab = 0;
            int iColonnetab = 0;
            int iCompteMot = 1;
            int iCompteMot2 = 0;
            int iNumeroDeMot = 0;
            int iNumeroDeMot2 = 0;
            int iNumeroDeMot3 = 0;
            int iDebut = 0;
            int iDebut1 = 0;
            int iDebut2 = 0;
            int iDebut3 = 0;


            Console.Clear();
            iCoordo = int.Parse(_sRepMot[0].ToString() + _sRepMot[1].ToString());
            iCoordo2 = int.Parse(_sRepMot[3].ToString() + _sRepMot[4].ToString());
            iCoordo3 = int.Parse(_sRepMot2[0].ToString() + _sRepMot2[1].ToString());
            iCoordo4 = int.Parse(_sRepMot2[3].ToString() + _sRepMot2[4].ToString());

            _listTrouver.Add(iCoordo);
            _listTrouver.Add(iCoordo2);
            _listTrouver.Add(iCoordo3);
            _listTrouver.Add(iCoordo4);


            for (int k = 0; k < _listMots[_iNumMot - 1].Length; k++)
            {
                iNumMot++;
            }

            iDebut = iCoordo;
            iDebut1 = iCoordo2;
            iDebut2 = iCoordo3;
            iDebut3 = iCoordo4;
            //Trouver les directions dans la grille
            for (int i = 0; i < _listTrouver.Count / 2; i++)
            {
                _abGrille[_listTrouver[iAxe], _listTrouver[iAYe]] = false;


                for (int j = 0; j < iNumMot - 2; j++)
                {

                    _abGrille[iDebut, iDebut1] = false;

                    if (iDebut1 == iDebut3)
                    {
                        if (iDebut2 > iDebut)
                        {
                            iDebut++;
                        }
                        if (iDebut > iDebut2)
                        {
                            iDebut--;
                        }

                    }

                    if (iDebut1 > iDebut3)
                    {
                        if (iDebut == iDebut2)
                        {

                            iDebut1--;

                        }

                    }
                    if (iDebut1 < iDebut3)
                    {
                        if (iDebut > iDebut2)
                        {
                            iDebut--;
                            iDebut1++;
                        }
                        else if (iDebut < iDebut2)
                        {
                            iDebut++;
                            iDebut1++;
                        }
                        else
                            iDebut1++;
                    }
                    if (iDebut > iDebut2)
                    {
                        if (iDebut1 == iDebut3)
                        {
                            iDebut++;
                            iDebut1++;
                        }
                        if (iDebut1 < iDebut3)
                        {
                            iDebut++;
                            iDebut1++;
                        }

                        iDebut--;
                        iDebut1--;

                    }
                    if (iDebut < iDebut2)
                    {

                        iDebut++;
                        iDebut1--;
                        if (iDebut3 > iDebut1)
                        {
                            iDebut--;
                            iDebut1++;
                        }
                        if (iDebut == iDebut2)
                        {
                            break;
                        }
                    }

                }
                iAxe += 2;
                iAYe += 2;

            }

            Console.WriteLine("     ***********************MOT-MYSTÈRE*********************** ");
            for (int k = 0; k < _acGrille.GetLength(0); k++)
            {
                if (iColonnetab == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("    " + iColonnetab.ToString("00") + " ");
                    Console.ResetColor();
                }
                else if (iColonnetab > 0)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(iColonnetab.ToString("00") + " ");
                    Console.ResetColor();
                }
                iColonnetab++;
            }
            Console.WriteLine();
            for (int m = 0; m < _acGrille.GetLength(0); m++)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(iLignetab.ToString("00") + "   ");
                Console.ResetColor();

                for (int j = 0; j < _acGrille.GetLength(1); j++)
                {

                    if (_abGrille[m, j] == false)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(_acGrille[m, j] + "  ");
                        Console.ResetColor();
                    }
                    else
                        Console.Write(_acGrille[m, j] + "  ");

                }
                iLignetab++;
                Console.WriteLine();
            }


            for (int i = 0; i < _listMots.Count; i++)
            {
                if (_listMots[i] == _listMots[_iNumMot - 1])
                {
                    _listbCouleur[i] = false;
                }
            }
            Console.WriteLine();
            Console.WriteLine("*****************************************");
            Console.WriteLine("Liste de mots à trouver (" + _iNum1 + " à 68) : ");
            Console.WriteLine("*****************************************");
            Console.WriteLine();

            for (int i = 0; i < 10; i++)
            {

                iNumeroDeMot2 = 0;
                iNumeroDeMot3 = 0;
                iCompteMot2 = 0;
                iCompteMot2 = iCompteMot + iCompteMot2;
                if (iCompteMot2 == 10)
                {

                    if (_listbCouleur[iNumeroDeMot] == false)
                    {
                        Console.BackgroundColor = ConsoleColor.Blue;
                        Console.Write("\n" + iCompteMot + ") " + _listMots[iNumeroDeMot].PadRight(19, ' '));
                        Console.ResetColor();

                    }
                    else
                        Console.Write("\n" + iCompteMot + ") " + _listMots[iNumeroDeMot].PadRight(19, ' '));
                }
                else if (iCompteMot2 == 20)
                {
                    if (_listbCouleur[iNumeroDeMot] == false)
                    {
                        Console.BackgroundColor = ConsoleColor.Blue;
                        Console.Write("\n" + iCompteMot + ") " + _listMots[iNumeroDeMot].PadRight(19, ' '));
                        Console.ResetColor();

                    }
                    else
                        Console.Write("\n" + iCompteMot + ") " + _listMots[iNumeroDeMot].PadRight(19, ' '));
                }
                else if (iCompteMot2 == 30)
                {
                    if (_listbCouleur[iNumeroDeMot] == false)
                    {
                        Console.BackgroundColor = ConsoleColor.Blue;
                        Console.Write("\n" + iCompteMot + ") " + _listMots[iNumeroDeMot].PadRight(19, ' '));
                        Console.ResetColor();

                    }
                    else
                        Console.Write("\n" + iCompteMot + ") " + _listMots[iNumeroDeMot].PadRight(19, ' '));
                }
                else if (iCompteMot2 == 40)
                {
                    if (_listbCouleur[iNumeroDeMot] == false)
                    {
                        Console.BackgroundColor = ConsoleColor.Blue;
                        Console.Write("\n" + iCompteMot + ") " + _listMots[iNumeroDeMot].PadRight(19, ' '));
                        Console.ResetColor();

                    }
                    else
                        Console.Write("\n" + iCompteMot + ") " + _listMots[iNumeroDeMot].PadRight(19, ' '));
                }
                else if (iCompteMot2 == 50)
                {
                    if (_listbCouleur[iNumeroDeMot] == false)
                    {
                        Console.BackgroundColor = ConsoleColor.Blue;
                        Console.Write("\n" + iCompteMot + ") " + _listMots[iNumeroDeMot].PadRight(19, ' '));
                        Console.ResetColor();

                    }
                    else
                        Console.Write("\n" + iCompteMot + ") " + _listMots[iNumeroDeMot].PadRight(19, ' '));
                }
                else if (iCompteMot2 == 60)
                {
                    if (_listbCouleur[iNumeroDeMot] == false)
                    {
                        Console.BackgroundColor = ConsoleColor.Blue;
                        Console.Write("\n" + iCompteMot + ") " + _listMots[iNumeroDeMot].PadRight(19, ' '));
                        Console.ResetColor();

                    }
                    else
                        Console.Write("\n" + iCompteMot + ") " + _listMots[iNumeroDeMot].PadRight(19, ' '));
                }

                if (iCompteMot > -1 && iCompteMot < 10)
                {
                    if (_listbCouleur[iNumeroDeMot] == false)
                    {
                        Console.BackgroundColor = ConsoleColor.Blue;
                        Console.Write("\n" + iCompteMot + ") " + _listMots[iNumeroDeMot].PadRight(20, ' '));
                        Console.ResetColor();

                    }
                    else
                        Console.Write("\n" + iCompteMot + ") " + _listMots[iNumeroDeMot].PadRight(20, ' '));

                }

                for (int j = 0; j < 6; j++)
                {
                    iCompteMot2 = iCompteMot2 + 10;
                    iNumeroDeMot2 = iNumeroDeMot2 + 10;
                    iNumeroDeMot3 = iNumeroDeMot + iNumeroDeMot2;

                    if (iNumeroDeMot3 > 68 || iCompteMot2 > 68)
                    {
                        break;
                    }
                    if (iCompteMot2 == 20 && iCompteMot2 == 30 && iCompteMot2 == 40 && iCompteMot2 == 50 && iCompteMot2 == 60)
                    {

                        if (_listbCouleur[iNumeroDeMot3] == false)
                        {
                            Console.BackgroundColor = ConsoleColor.Blue;
                            Console.Write(iCompteMot2 + ") " + _listMots[iNumeroDeMot3].PadRight(19, ' '));
                            Console.ResetColor();
                        }
                        else
                            Console.Write(iCompteMot2 + ") " + _listMots[iNumeroDeMot3].PadRight(19, ' '));
                    }
                    if (_listbCouleur[iNumeroDeMot3] == false)
                    {
                        Console.BackgroundColor = ConsoleColor.Blue;
                        Console.Write(iCompteMot2 + ") " + _listMots[iNumeroDeMot3].PadRight(20, ' '));
                        Console.ResetColor();

                    }
                    else
                        Console.Write(iCompteMot2 + ") " + _listMots[iNumeroDeMot3].PadRight(20, ' '));

                }
                iCompteMot++;
                iNumeroDeMot++;
            }

            do
            {
                Console.WriteLine();
                Console.Write("Entrer le numéro du mot : ");
                sReponse = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(sReponse.ToUpper()))
                {
                    Console.WriteLine("Veuillez entrer une réponse valide.");
                }

                for (int i = 0; i < sReponse.Length; i++)
                {
                    cRep = sReponse[i];
                    if (char.IsLetter(cRep))
                    {
                        if (sReponse.ToUpper() == _sReponse.ToUpper())
                        {
                            Console.WriteLine("Bravo!");
                            break;
                        }
                        else if (sReponse.ToUpper() != _sReponse.ToUpper())
                        {
                            Console.WriteLine("ERREUR: Vous n'avez pas trouvé le bon mot");
                            break;
                        }

                    }
                    if (char.IsNumber(cRep))
                    {
                        _iNumMot = int.Parse(sReponse);
                        if (_iNumMot > 0 && _iNumMot < 69)
                        {
                            do
                            {
                                Console.Write("Entrer la coordonnée de la première lettre du mot " + _listMots[_iNumMot - 1] + ":");
                                _sRepMot = Console.ReadLine();


                                iVerifieChiffre = int.Parse(_sRepMot[0].ToString() + _sRepMot[1].ToString());
                                if (iVerifieChiffre > _acGrille.GetLength(0))
                                {
                                    Console.WriteLine("Veuillez entrer une coordonnée valide!");
                                    bVerifie = false;

                                }
                                else
                                    bVerifie = true;


                                iVerifieChiffre = int.Parse(_sRepMot[3].ToString() + _sRepMot[4].ToString());
                                if (iVerifieChiffre > _acGrille.GetLength(0))
                                {
                                    Console.WriteLine("Veuillez entrer une coordonnée valide!");
                                    bVerifie = false;

                                }
                                else
                                    bVerifie = true;


                            } while (bVerifie != true);

                            do
                            {

                                Console.Write("Entrer la coordonnée de la dernière lettre du mot " + _listMots[_iNumMot - 1] + ":");
                                _sRepMot2 = Console.ReadLine();

                                iVerifieChiffre = int.Parse(_sRepMot2[0].ToString() + _sRepMot2[1].ToString());
                                if (iVerifieChiffre > _acGrille.GetLength(0))
                                {
                                    Console.WriteLine("Veuillez entrer une coordonnée valide!");
                                    bVerifie = false;

                                }
                                else
                                    bVerifie = true;


                                iVerifieChiffre = int.Parse(_sRepMot2[3].ToString() + _sRepMot2[4].ToString());
                                if (iVerifieChiffre > _acGrille.GetLength(0))
                                {
                                    Console.WriteLine("Veuillez entrer une coordonnée valide!");
                                    bVerifie = false;

                                }
                                else
                                    bVerifie = true;


                            } while (bVerifie != true);

                            break;

                        }
                        else if (_iNumMot > 68)
                        {
                            bVerifie = false;
                            Console.WriteLine("ERREUR: le numéro du mot ne correspond pas!");
                            break;
                        }
                    }

                }

            } while (sReponse.ToUpper() != _sReponse.ToUpper() && bVerifie != true);

            if (sReponse.ToUpper() == _sReponse.ToUpper())
            {
                Console.ReadKey();
            }
            else
                MotGrille();
        }
    }
}
