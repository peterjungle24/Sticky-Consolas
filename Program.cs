using SluggHelpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.Json.Nodes;
using static System.Runtime.InteropServices.JavaScript.JSType;

// Sticky Consolas = Sticky Notes as Console
// i am fuckass on names.
namespace StickyConsolas
{
    public class Program
    {
        private static JsonArray indexColorNode, collectionColorNode, noteColorNode, checkedColorNode, uncheckedColorNode;
        private static ConsolasColor indexColor, collectionColor, noteColor, checkedColor, uncheckedColor;

        static void Main(string[] args)
        {
            SetSettings();

            // reads the whole file and stores on a single string
            var file = File.ReadAllText("notes.json");
            // parse the string as a Json thing
            var json = JsonNode.Parse(file).AsObject();

            while (true)
            {
                // shows all the collections, and then gets a list full of indexes!
                var collections = ShowCollections(json);
                // print this.
                Console.WriteLine("\n> ");
                // asks for a input (expects a number)
                var input = Console.ReadLine();

                // tries to get a integer from the input (expects a index)
                if (int.TryParse(input, out int value) )
                {
                    Console.Clear();    // clears console
                    int index = 0;      // really cool index for the foreach

                    // loops on the JSON variable
                    foreach (var item in json)
                    {
                        // if the index is the same as the value of the collectio ns
                        if (index == collections[value] )
                        {
                            // prints a intro
                            Consolas.ColorWriteLine($"{Consolas.ITALIC}{ConsolasColor.gray}Collection: {item.Key }\x1b[0m\n");
                            // shows the note from the input
                            ShowNotes(json, value);
                        }
                        // increases the nice indexer
                        index++;
                    }
                }
            }
        }

        static void SetSettings()
        {
            // reads the whole file and stores on a single string
            var file = File.ReadAllText("settings.json");
            // parse the string as a Json thing
            var settings = JsonNode.Parse(file);
            // gets the "colors" object
            var colors = settings["colors"] as JsonObject;

            // initialize the fields.
            indexColorNode = colors["index_color"] as JsonArray;
            collectionColorNode = colors["collection_color"] as JsonArray;
            noteColorNode = colors["note_color"] as JsonArray;
            checkedColorNode = colors["checked_color"] as JsonArray;
            uncheckedColorNode = colors["unchecked_color"] as JsonArray;

            // just some exceptions.
            if (indexColorNode.Count < 3 || indexColorNode.Count > 3)
                throw new ArgumentOutOfRangeException("The 'index_color' array expected atleast 3 values.");
            if (collectionColorNode.Count < 3 || collectionColorNode.Count > 3)
                throw new ArgumentOutOfRangeException("The 'collection_color' array expected atleast 3 values.");
            if (noteColorNode.Count < 3 || noteColorNode.Count > 3)
                throw new ArgumentOutOfRangeException("The 'note_color' array expected atleast 3 values.");
            if (checkedColorNode.Count < 3 || checkedColorNode.Count > 3)
                throw new ArgumentOutOfRangeException("The 'checked_color' array expected atleast 3 values.");
            if (uncheckedColorNode.Count < 3 || uncheckedColorNode.Count > 3)
                throw new ArgumentOutOfRangeException("The 'unchecked_color' array expected atleast 3 values.");

            // set colors here
            indexColor = ConsolasColor.RGB((int)indexColorNode[0], (int)indexColorNode[1], (int)indexColorNode[2]);
            collectionColor = ConsolasColor.RGB((int)collectionColorNode[0], (int)collectionColorNode[1], (int)collectionColorNode[2]);
            noteColor = ConsolasColor.RGB((int)noteColorNode[0], (int)noteColorNode[1], (int)noteColorNode[2]);
            checkedColor = ConsolasColor.RGB((int)checkedColorNode[0], (int)checkedColorNode[1], (int)checkedColorNode[2]);
            uncheckedColor = ConsolasColor.RGB((int)uncheckedColorNode[0], (int)uncheckedColorNode[1], (int)uncheckedColorNode[2]);
        }
        static List<int> ShowCollections(JsonObject json)
        {
            // a nice list for return and get element
            List<int> list = new List<int>();
            // a nice indexer for the foreach loop
            var indexer = 0;
            // goes on the loop of the json file
            foreach (var jsonItem in json)
            {
                // prints all
                Consolas.ColorWriteLine($"{indexColor}[{indexer}]-> {collectionColor}{jsonItem.Key}");
                // adds a number to the list
                list.Add(indexer);
                // adds 1 each time
                indexer++;
            }

            // returns my wonderfull list
            return list;
        }
        static void ShowNotes(JsonObject json, int indexer)
        {
            // a cool indexer for the foreach loop
            int index = 0;
            
            // goes on the loop of the json file
            // NESTED FOREACH LOOPS GOES WEEEEEEEEEEEEEEEEEEEEEEEEEEEEE
            foreach (var jsonItem in json)
            {
                // if the current item is a array
                if (jsonItem.Value is JsonArray arr)
                {
                    // loops on the array
                    foreach (JsonObject valueItem in arr)
                    {
                        // if the source index is the same as the index
                        // prefered fields to be seen
                        if (index == indexer)
                        {
                            // if the "checked" is False, then its "NOT DONE"
                            if ((bool)valueItem["checked"] == false)
                                // prints the note with "unchecked_color"
                                Consolas.ColorWriteLine($"{noteColor}{valueItem["text"]} {uncheckedColor}(NOT DONE)");
                            // otherwise, its "DONE"
                            else if ((bool)valueItem["checked"] == true)
                                // prints the note with "checked_color"
                                Consolas.ColorWriteLine($"{noteColor}{valueItem["text"]} {checkedColor}(DONE)");
                        }
                    }
                }
                
                index++;
            }
            // prints this one
            Consolas.ColorWriteLine($"{Consolas.BLINK}{ConsolasColor.gray}\nPress any key to back.");
            // asks for a input, and then clears the chat
            Console.ReadKey(true); Console.Clear();
            // IS.OVER.BRO
            return;
        }
    }
}