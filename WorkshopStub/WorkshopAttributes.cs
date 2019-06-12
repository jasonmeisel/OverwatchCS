using System;

// taken from https://us.forums.blizzard.com/en/overwatch/t/wiki-workshop-syntax-script-database/335011/3
// using: $("details").map((i, e) => $(e).text().trim().split("\n").map(s => "// " + s).join("\n") + "\n[WorkshopActionName(" + $(e).find("summary")[0].innerText.trim() + ")]\npublic static void " + $(e).find("summary")[0].innerText.trim().split(" ").map(s => s[0] + s.substr(1).toLowerCase()).join("") + "()").toArray().join("\n")

// regex helpers:
// "(?!        )(\/\/\/[\s\S\r]*?)(?=        \[Workshop)"
// "/// <summary>\n        $1        /// </summary>\n"

public class WorkshopCodeAttribute : Attribute
{
    public string Name;

    public WorkshopCodeAttribute(string name)
    {
        Name = name;
    }
}

public class WorkshopEventAttribute : Attribute
{
    public string Text;

    public WorkshopEventAttribute(string text)
    {
        Text = text;
    }
}
