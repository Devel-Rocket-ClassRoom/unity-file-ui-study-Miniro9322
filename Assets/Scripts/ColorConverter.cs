using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ColorConverter : JsonConverter<Color>
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override Color ReadJson(JsonReader reader, Type objectType, Color existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        Color c = Color.clear;

        JObject jObj = JObject.Load(reader);
        c.r = (float)jObj["R"];
        c.g = (float)jObj["G"];
        c.b = (float)jObj["B"];
        c.a = (float)jObj["A"];

        return c;
    }

    public override void WriteJson(JsonWriter writer, Color value, JsonSerializer serializer)
    {
        JObject jObj = new JObject()
        {
            ["R"] = value.r,
            ["G"] = value.g,
            ["B"] = value.b,
            ["A"] = value.a
        };
        jObj.WriteTo(writer);
    }
}
