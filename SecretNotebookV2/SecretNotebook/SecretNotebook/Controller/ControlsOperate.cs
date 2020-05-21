using SecretNotebook.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SecretNotebook.Controller.Handlers;
using SecretNotebook.View;

namespace SecretNotebook.Controller
{
    public static partial class Controls
    {
        public static void CheckPassword()
        {
            var dh = new DecodeHandler();

            if (!File.Exists(ConstantKeeper.PathToKeys))
            {
                FirstLaunch();
                if ((ConstantKeeper.CurrentArea is YesNoArea yn && !yn.Yes) ||
                    !(ConstantKeeper.CurrentArea is YesNoArea))
                {
                    ConstantKeeper.CurrentArea = new Area(
                        "Файл с ключом не найден! Введите путь до файла с ключом:",
                        () =>
                        {
                            ConstantKeeper.PathToKeys = Console.ReadLine();
                            CheckPassword();
                        });
                }
                else
                {
                    ChangePassword();
                    // TODO: переделать
                    ConstantKeeper.CurrentArea = new MenuArea(
                        "Главное меню:",
                        () =>
                        {
                            ConstantKeeper.CurrentSource = new ModelSource();
                            ConstantKeeper.CurrentSource.AddNote("Ваша первая заметка", 
                                "Добавляйте заметки в меню, с помощью кнопки 'a'...");

                            ConstantKeeper.SetIV(NotebookCryptography.GenerateIV());
                            ConstantKeeper.CurrentSource.SerializeNotes();

                            foreach (var el in ConstantKeeper.CurrentSource.Notes)
                            {
                                Console.WriteLine(el.Name);
                            }
                        });
                }
            }
            else if (!File.Exists(ConstantKeeper.PathToNotes))
            {
                ConstantKeeper.CurrentArea = new Area(
                       "Файл с записями не найден! Введите путь до файла с записями:",
                       () =>
                       {
                           ConstantKeeper.PathToNotes = Console.ReadLine();
                           CheckPassword();
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

                               //TODO:переделать
                               //ConstantKeeper.CurrentArea = new MenuArea(
                               //    "Главное меню:",
                               //     () =>
                               //     {
                               //         ConstantKeeper.CurrentSource = new ModelSource();
                               //         ConstantKeeper.CurrentSource.AddNote("Ваша первая заметка",
                               //             "Добавляйте заметки в меню, с помощью кнопки 'a'...");

                               //         ConstantKeeper.SetIV(NotebookCryptography.GenerateIV());
                               //         ConstantKeeper.CurrentSource.SerializeNotes();

                               //         foreach (var el in ConstantKeeper.CurrentSource.Notes)
                               //         {
                               //             Console.WriteLine(el.Name);
                               //         }
                               //     });
                           }
                           else
                           {
                               Console.WriteLine("Пароль введен не верно!");
                               CheckPassword();
                           }
                       });
            }

            dh.EnterPassword += (sender, e) => ConstantKeeper.CurrentArea.Run();
            dh.OnEnterPassword(new EventArgs());

        }

        private static void FirstLaunch()
        {
            var fl = new FirstLaunchHandler();

            ConstantKeeper.CurrentArea = new YesNoArea(
                "Ключ не найден. Это первый запуск? (Y - да)",
                () =>
                {
                    var answer = Console.ReadLine();
                    if (answer == "Y" || answer == "y")
                    {
                        if (ConstantKeeper.CurrentArea is YesNoArea yn) yn.Yes = true;
                    }
                });

            fl.FirstLaunch += (sender, e) => ConstantKeeper.CurrentArea.Run();
            fl.OnFirstLaunch(new EventArgs());
        }

        public static void ChangePassword()
        {
            var cp = new ChangePasswordHandler();

            ConstantKeeper.CurrentArea = new YesNoArea(
                "Введите новый пароль:",
                () =>
                {
                    var password = Console.ReadLine();
                    var hash = NotebookCryptography.GetHash(password);
                    ConstantKeeper.SetKey(NotebookCryptography.GenerateKey());

                    NotebookModelIO.SaveHashKey(hash, ConstantKeeper.Key);
                });

            cp.ChangePassword += (sender, e) => ConstantKeeper.CurrentArea.Run();
            cp.OnChangePassword(new EventArgs());
        }
    }
}
