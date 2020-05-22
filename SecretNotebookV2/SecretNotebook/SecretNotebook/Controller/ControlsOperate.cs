using SecretNotebook.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SecretNotebook.Controller.Handlers;
using SecretNotebook.View;
using System.Threading;

namespace SecretNotebook.Controller
{
    public static partial class Controls
    {
        private static void FirstLaunchFill()
        {
            ConstantKeeper.CurrentSource = new ModelSource();
            ConstantKeeper.CurrentSource.AddNote("Ваша первая заметка",
                "Ключ, расшифровывающий записи, можно хранить, например, на флешке...");
        }

        public static void Authentification()
        {
            var dh = new AuthentificationHandler();

            if (!File.Exists(ConstantKeeper.PathToKeys))
            {
                CreateQuestion("Ключ не найден. Это первый запуск? (Y - да)");
                if ((ConstantKeeper.CurrentArea is YesNoArea yn && !yn.Yes) ||
                    !(ConstantKeeper.CurrentArea is YesNoArea))
                {
                    ConstantKeeper.CurrentArea = new Area(
                        "Файл с ключом не найден! Введите путь до файла с ключом:",
                        () =>
                        {
                            ConstantKeeper.PathToKeys = Console.ReadLine();
                            Authentification();
                        });
                }
                else
                {
                    ConstantKeeper.CurrentArea = new Area(
                        "Подготовка...", ChangePassword);
                }
            }
            else if (!File.Exists(ConstantKeeper.PathToNotes))
            {
                ConstantKeeper.CurrentArea = new Area(
                       "Файл с записями не найден! Введите путь до файла с записями:",
                       () =>
                       {
                           ConstantKeeper.PathToNotes = Console.ReadLine();
                           Authentification();
                       });
            }
            else 
            {
                ConstantKeeper.CurrentArea = new Area(
                       "Введите пароль:",
                       () =>
                       {
                           var password = Console.ReadLine();

                           bool isCorrect = NotebookCryptography.CompareHash(
                               password,
                               NotebookModelIO.ReadHashKey(true));

                           if (isCorrect)
                           {
                               ConstantKeeper.SetKey(NotebookModelIO.ReadHashKey(false));
                               ConstantKeeper.CurrentSource = new ModelSource();
                               ConstantKeeper.CurrentSource.DeSerializeNotes();
                           }
                           else
                           {
                               Console.WriteLine("Пароль введен не верно!");
                               Thread.Sleep(1000);
                               Authentification();
                           }
                       });
            }

            dh.EnterPassword += (sender, e) => ConstantKeeper.CurrentArea.Run();
            dh.OnEnterPassword(new EventArgs());

        }

        private static void CreateQuestion(string question)
        {
            var fl = new QuestionHandler();

            ConstantKeeper.CurrentArea = new YesNoArea(
                question,
                () =>
                {
                    var answer = Console.ReadLine();
                    if (answer == "Y" || answer == "y")
                    {
                        if (ConstantKeeper.CurrentArea is YesNoArea yn) yn.Yes = true;
                    }
                });

            fl.SetQuestion += (sender, e) => ConstantKeeper.CurrentArea.Run();
            fl.OnSetQuestion(new EventArgs());
        }

        public static void ChangePassword()
        {
            var cp = new ChangePasswordHandler();

            ConstantKeeper.CurrentArea = new Area(
                "Введите новый пароль:",
                () =>
                {
                    var password = Console.ReadLine();
                    var hash = NotebookCryptography.GetHash(password);
                    ConstantKeeper.SetKey(NotebookCryptography.GenerateKey());

                    NotebookModelIO.SaveHashKey(hash, ConstantKeeper.Key);

                    OverwriteNotes();
                });

            cp.ChangePassword += (sender, e) => ConstantKeeper.CurrentArea.Run();
            cp.OnChangePassword(new EventArgs());
        }

        public static void OpenMenu()
        {
            var mm = new MenuHandler();

            var currentKey = Console.ReadKey();

            //сначала фактические изменения
            if(Menu.TryGetValue(currentKey.Key, out var action))
            {
                mm.MenuAction += (sender, e) => action.Item2();
            }

            //потом отрисовка
            //mm.MenuAction += (sender, e) => ConstantKeeper.CurrentArea.Run();
            mm.OnMenuAction(new EventArgs());
        }

        public static void AddNote()
        {
            var an = new AddNoteHandler();

            ConstantKeeper.CurrentArea = new Area(
                "Введите текст заметки:",
                () =>
                {
                    var note = Console.ReadLine();
                    Console.Clear();

                    Console.WriteLine("Введите название");
                    var name = Console.ReadLine();
                    ConstantKeeper.CurrentSource.AddNote(name, note);
                    OverwriteNotes();
                });

            an.AddNote += (sender, e) => ConstantKeeper.CurrentArea.Run();
            an.OnAddNote(new EventArgs());
        }

        public static void OverwriteNotes()
        {
            var ov = new OverwriteHandler();

            if(ConstantKeeper.CurrentSource == null || 
                ConstantKeeper.CurrentSource.Notes.Count == 0)
            {
                ov.Overwrite += (sender, e) => FirstLaunchFill();
            }

            ov.Overwrite += (sender, e) =>
            {
                ConstantKeeper.SetIV(NotebookCryptography.GenerateIV());
                ConstantKeeper.CurrentSource.SerializeNotes();
            };

            ov.OnOverwrite(new EventArgs());
        }

        public static void OpenNotes()
        {
            var on = new OpenNotesHandler();

            if (ConstantKeeper.CurrentArea is MenuArea area)
            {
                int Indx = area.GetMenuCounter;
                string name = ConstantKeeper.CurrentSource.Notes[Indx].Name;
                string txt = ConstantKeeper.CurrentSource.Notes[Indx].Txt;
                var datetime = ConstantKeeper.CurrentSource.Notes[Indx].Date;

                ConstantKeeper.CurrentArea = new Area(
                name + " " + datetime + "\n" + txt,
                () =>
                {
                    Console.ReadLine();
                });
            }
            else
            {
                return;
            }

            on.OpenNotes += (sender, e) => ConstantKeeper.CurrentArea.Run();
            on.OnOpenNotes(new EventArgs());
        }

        public static void RenameNote()
        {
            var rh = new RenameNotesHandler();

            if (ConstantKeeper.CurrentArea is MenuArea area)
            {
                int Indx = area.GetMenuCounter;
                var note = ConstantKeeper.CurrentSource.Notes[Indx];

                ConstantKeeper.CurrentArea = new Area(
                "Переименуйте заметку " + note.Name + ":",
                () =>
                {
                    note.Name = Console.ReadLine();
                    note.Date = DateTime.Now;
                    OverwriteNotes();
                });
            }
            else
            {
                return;
            }

            rh.RenameNote += (sender, e) => ConstantKeeper.CurrentArea.Run();
            rh.OnRenameNote(new EventArgs());
        }

        public static void DeleteNote()
        {
            var dn = new DeleteNotesHandler();

            if (ConstantKeeper.CurrentArea is MenuArea area)
            {
                int Indx = area.GetMenuCounter;
                var note = ConstantKeeper.CurrentSource.Notes[Indx];

                CreateQuestion("Удалить заметку " + note.Name + "? (Y - да)");

                if (ConstantKeeper.CurrentArea is YesNoArea yn && yn.Yes)
                {
                    ConstantKeeper.CurrentArea = new Area(
                        "Заметка удалена, нажмите Enter, чтобы продолжить:",
                        () =>
                        {
                            ConstantKeeper.CurrentSource.Notes.Remove(note);
                            OverwriteNotes();
                            Console.ReadLine();
                        });
                }
                else
                {
                    return;
                }
            }
            else
            {
                return;
            }

            dn.DeleteNote += (sender, e) => ConstantKeeper.CurrentArea.Run();
            dn.OnDeleteNote(new EventArgs());
        }
    }
}
