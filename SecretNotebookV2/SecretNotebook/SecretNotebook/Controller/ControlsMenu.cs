using SecretNotebook.Model;
using SecretNotebook.View;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecretNotebook.Controller
{
    public static partial class Controls
    {
        public static Dictionary<ConsoleKey, ValueTuple<string, Action>> Menu =
            new Dictionary<ConsoleKey, ValueTuple<string, Action>>
            {
                { ConsoleKey.Insert,    new ValueTuple<string, Action>(
                                        "Insert - добавить заметку",
                                        AddNote) },

                { ConsoleKey.R,         new ValueTuple<string, Action>(
                                        "R - переименовать заметку",
                                        RenameNote) },

                { ConsoleKey.UpArrow,   new ValueTuple<string, Action>(
                                        "UpArrow - навигация",
                                        ((MenuArea)ConstantKeeper.CurrentArea).MenuUp) },

                { ConsoleKey.DownArrow, new ValueTuple<string, Action>(
                                        "DownArrow - навигация", 
                                        ((MenuArea)ConstantKeeper.CurrentArea).MenuDown) },

                { ConsoleKey.Delete,    new ValueTuple<string, Action>(
                                        "Delete - удалить заметку",
                                        DeleteNote) },

                { ConsoleKey.Enter,     new ValueTuple<string, Action>(
                                        "Enter - открыть заметку",
                                        OpenNotes) },

                { ConsoleKey.C,         new ValueTuple<string, Action>(
                                        "C - изменить пароль",
                                        ChangePassword) },

                //{ ConsoleKey.S,         new ValueTuple<string, Action>(
                //                        "S - сохранить изменения на диск", 
                //                        Console.Beep) },
            };
    }
}
