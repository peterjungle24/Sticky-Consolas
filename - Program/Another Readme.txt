%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
			Notes.json

On this file, all the notes are inside of collections, and each collection is a "array" of Objects
{
    // the collection name can have any name.
    "my collection name": [
        // is REQUIRED to have a "text:" and "checked:" properties.
        {
            "text": "My note here!",
            "checked": false
        }
    ]
}
Each collection element have 2 properties: "text" and "checked".

- "text: <string>" -> Its a text that will be shown after selecting a collection.
  - This one CAN BE ANYTHING YOU WANT, since its a string type.
- "checked: <bool>" -> Its a property that checks if the "task" has been done.
  - If "false", then it will show "NOT DONE". If "true", then it will show "DONE".

%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
			text

Its a string that shows on the console, after you selected a Collection to show on screen.
The text can be anything, but i recommend it for make descriptive texts as notes, since this program was planned in mind for notes.

----------------------------------------------
"My Collection": [
    {"text": "My text here", "checked": true}
]
----------------------------------------------
----------------------------------------------
My text here (DONE)
----------------------------------------------
%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
			checked

A ``bool`` property that checks if its done or not
- Can be very good for know if the *task* is done or not.
  - Note that you need to change it manually, on current version.

i am not good at C#, sorry guys.