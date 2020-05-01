using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Linq;

namespace TribalSvcPortal.AppLogic.BusinessLogicLayer
{
    public static class Utils {

        public class ConfigInfoType
        {
            public string _name { get; set; }
            public string _req { get; set; }
            public int? _length { get; set; }
            public string _datatype { get; set; }
            public string _fkey { get; set; }
            public string _addfield { get; set; }
        }


        /// <summary>
        ///  Converts to another datatype or returns default value
        /// </summary>
        public static T ConvertOrDefault<T>(this object value)
        {
            T result = default(T);
            Utils.TryConvert<T>(value, out result);
            return result;
        }

        public static TResult GetPropertyValue<TResult>(this object t, string propertyName)
        {
            object val = t.GetType().GetProperties().Single(pi => pi.Name == propertyName).GetValue(t, null);
            return (TResult)val;
        }

        /// <summary>
        ///  Better than built-in SubString by handling cases where string is too short
        /// </summary>
        public static string SubStringPlus(this string str, int index, int length)
        {
            if (index >= str.Length)
                return String.Empty;

            if (index + length > str.Length)
                return str.Substring(index);

            return str.Substring(index, length);
        }

        /// <summary>
        ///  Generic data type converter 
        /// </summary>
        public static bool TryConvert<T>(object value, out T result)
        {
            result = default(T);
            if (value == null || value == DBNull.Value) return false;

            if (typeof(T) == value.GetType())
            {
                result = (T)value;
                return true;
            }

            string typeName = typeof(T).Name;

            try
            {
                if (typeName.IndexOf(typeof(System.Nullable).Name, StringComparison.Ordinal) > -1 ||
                    typeof(T).BaseType.Name.IndexOf(typeof(System.Enum).Name, StringComparison.Ordinal) > -1)
                {
                    TypeConverter tc = TypeDescriptor.GetConverter(typeof(T));
                    result = (T)tc.ConvertFrom(value);
                }
                else
                    result = (T)Convert.ChangeType(value, typeof(T));
            }
            catch
            {
                return false;
            }

            return true;
        }

        public static string GetSafeHtml(string html, bool useXssSantiser = false)
        {
            // Scrub html
            html = ScrubHtml(html, useXssSantiser);

            // remove unwanted html
            html = RemoveUnwantedTags(html);

            return html;
        }

        public static string ScrubHtml(string html, bool useXssSantiser = false)
        {
            if (string.IsNullOrEmpty(html))
            {
                return html;
            }

            // clear the flags on P so unclosed elements in P will be auto closed.
            HtmlNode.ElementsFlags.Remove("p");

            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            var finishedHtml = html;

            // Embed Urls
            if (doc.DocumentNode != null)
            {
                // Get all the links we are going to 
                var tags = doc.DocumentNode.SelectNodes("//a[contains(@href, 'youtube.com')]|//a[contains(@href, 'youtu.be')]|//a[contains(@href, 'vimeo.com')]|//a[contains(@href, 'screenr.com')]|//a[contains(@href, 'instagram.com')]");

                if (tags != null)
                {
                    // find formatting tags
                    foreach (var item in tags)
                    {
                        if (item.PreviousSibling == null)
                        {
                            // Prepend children to parent node in reverse order
                            foreach (var node in item.ChildNodes.Reverse())
                            {
                                item.ParentNode.PrependChild(node);
                            }
                        }
                        else
                        {
                            // Insert children after previous sibling
                            foreach (var node in item.ChildNodes)
                            {
                                item.ParentNode.InsertAfter(node, item.PreviousSibling);
                            }
                        }

                        // remove from tree
                        item.Remove();
                    }
                }


                //Remove potentially harmful elements
                var nc = doc.DocumentNode.SelectNodes("//script|//link|//iframe|//frameset|//frame|//applet|//object|//embed");
                if (nc != null)
                {
                    foreach (var node in nc)
                    {
                        node.ParentNode.RemoveChild(node, false);

                    }
                }

                //remove hrefs to java/j/vbscript URLs
                nc = doc.DocumentNode.SelectNodes("//a[starts-with(translate(@href, 'ABCDEFGHIJKLMNOPQRSTUVWXYZ', 'abcdefghijklmnopqrstuvwxyz'), 'javascript')]|//a[starts-with(translate(@href, 'ABCDEFGHIJKLMNOPQRSTUVWXYZ', 'abcdefghijklmnopqrstuvwxyz'), 'jscript')]|//a[starts-with(translate(@href, 'ABCDEFGHIJKLMNOPQRSTUVWXYZ', 'abcdefghijklmnopqrstuvwxyz'), 'vbscript')]");
                if (nc != null)
                {

                    foreach (var node in nc)
                    {
                        node.SetAttributeValue("href", "#");
                    }
                }

                //remove img with refs to java/j/vbscript URLs
                nc = doc.DocumentNode.SelectNodes("//img[starts-with(translate(@src, 'ABCDEFGHIJKLMNOPQRSTUVWXYZ', 'abcdefghijklmnopqrstuvwxyz'), 'javascript')]|//img[starts-with(translate(@src, 'ABCDEFGHIJKLMNOPQRSTUVWXYZ', 'abcdefghijklmnopqrstuvwxyz'), 'jscript')]|//img[starts-with(translate(@src, 'ABCDEFGHIJKLMNOPQRSTUVWXYZ', 'abcdefghijklmnopqrstuvwxyz'), 'vbscript')]");
                if (nc != null)
                {
                    foreach (var node in nc)
                    {
                        node.SetAttributeValue("src", "#");
                    }
                }

                //remove on<Event> handlers from all tags
                nc = doc.DocumentNode.SelectNodes("//*[@onclick or @onmouseover or @onfocus or @onblur or @onmouseout or @ondblclick or @onload or @onunload or @onerror]");
                if (nc != null)
                {
                    foreach (var node in nc)
                    {
                        node.Attributes.Remove("onFocus");
                        node.Attributes.Remove("onBlur");
                        node.Attributes.Remove("onClick");
                        node.Attributes.Remove("onMouseOver");
                        node.Attributes.Remove("onMouseOut");
                        node.Attributes.Remove("onDblClick");
                        node.Attributes.Remove("onLoad");
                        node.Attributes.Remove("onUnload");
                        node.Attributes.Remove("onError");
                    }
                }

                // remove any style attributes that contain the word expression (IE evaluates this as script)
                nc = doc.DocumentNode.SelectNodes("//*[contains(translate(@style, 'ABCDEFGHIJKLMNOPQRSTUVWXYZ', 'abcdefghijklmnopqrstuvwxyz'), 'expression')]");
                if (nc != null)
                {
                    foreach (var node in nc)
                    {
                        node.Attributes.Remove("stYle");
                    }
                }

                // build a list of nodes ordered by stream position
                var pos = new NodePositions(doc);

                // browse all tags detected as not opened
                foreach (var error in doc.ParseErrors.Where(e => e.Code == HtmlParseErrorCode.TagNotOpened))
                {
                    // find the text node just before this error
                    var last = pos.Nodes.OfType<HtmlTextNode>().LastOrDefault(n => n.StreamPosition < error.StreamPosition);
                    if (last != null)
                    {
                        // fix the text; reintroduce the broken tag
                        last.Text = error.SourceText.Replace("/", "") + last.Text + error.SourceText;
                    }
                }

                finishedHtml = doc.DocumentNode.WriteTo();
            }


            return finishedHtml;
        }

        public static string RemoveUnwantedTags(string html)
        {

            var unwantedTagNames = new List<string>
            {
                "div",
                "font",
                "table",
                "tbody",
                "tr",
                "td",
                "th",
                "thead"
            };

            if (string.IsNullOrEmpty(html))
            {
                return html;
            }

            var htmlDoc = new HtmlDocument();

            // load html
            htmlDoc.LoadHtml(html);

            var tags = (from tag in htmlDoc.DocumentNode.Descendants()
                        where unwantedTagNames.Contains(tag.Name)
                        select tag).Reverse();


            // find formatting tags
            foreach (var item in tags)
            {
                if (item.PreviousSibling == null)
                {
                    // Prepend children to parent node in reverse order
                    foreach (var node in item.ChildNodes.Reverse())
                    {
                        item.ParentNode.PrependChild(node);
                    }
                }
                else
                {
                    // Insert children after previous sibling
                    foreach (var node in item.ChildNodes)
                    {
                        item.ParentNode.InsertAfter(node, item.PreviousSibling);
                    }
                }

                // remove from tree
                item.Remove();
            }

            // return transformed doc
            return htmlDoc.DocumentNode.WriteContentTo().Trim();
        }

        /// <summary>
        /// Returns true if the file is an image based on the file extension
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static bool FileIsImage(string file)
        {
            var imageFileTypes = new List<string>
            {
                ".jpg", ".jpeg",".gif",".bmp",".png"
            };
            return imageFileTypes.Any(file.Contains);
        }



        //******************* DATA IMPORT HELPERS**********************************
        public static Dictionary<string, int> GetColumnMapping(string ImportType, string[] headerCols, string path)
        {
            // Loading configuration file listing all data import columns
            var xml = XDocument.Load(path);

            // Query list of all columns for the type
            var allFields = (from c in xml.Root.Descendants("Alias")
                          .Where(i => i.Parent.Attribute("Level").Value == ImportType)
                             select new
                             {
                                 Name = c.Parent.Attribute("FieldName").Value,
                                 Alias = c.Value.ToUpper()
                             }).ToList();

            //list of fields supplied by user
            var headerColList = headerCols.Select((value, index) => new { value, index }).ToList();

            //return matches with index
            var colMapping = (from f in allFields
                              join h in headerColList
                              on f.Alias.Trim() equals h.value.ToUpper().Trim()
                              select new
                              {
                                  _Name = f.Name.Trim(),
                                  _Col = h.index
                              }).ToDictionary(x => x._Name, x => x._Col.ConvertOrDefault<int>());

            return colMapping;
        }

        public static List<ConfigInfoType> GetAllColumnInfo(string ImportType, string path)
        {
            // Loading configuration file listing all data import columns
            var xml = XDocument.Load(path);

            // Query list of all columns for the type
            return (from c in xml.Root.Descendants("Alias")
                    .Where(i => i.Parent.Attribute("Level").Value == ImportType)
                    select new ConfigInfoType
                    {
                        _name = c.Parent.Attribute("FieldName").Value,
                        _req = c.Parent.Attribute("ReqInd").Value,
                        _length = c.Parent.Attribute("Length").Value.ConvertOrDefault<int?>(),
                        _datatype = c.Parent.Attribute("DataType").Value,
                        _fkey = c.Parent.Attribute("FKey").Value,
                        _addfield = c.Parent.Attribute("AddField") != null ? c.Parent.Attribute("AddField").Value : ""
                    }).ToList();
        }

        public static List<string> GetMandatoryImportFieldList(string TableName, string path)
        {
            // Loading configuration file listing all data import columns
            var xml = XDocument.Load(path);

            // Query list of all columns for the type
            List<string> mandFields = (from c in xml.Root.Descendants("Alias")
                                       .Where(i => i.Parent.Attribute("TableName").Value == TableName)
                                       .Where(j => j.Parent.Attribute("ReqInd").Value == "Y")
                                       select c.Parent.Attribute("FieldName").Value
                                       ).ToList();
            return mandFields;
        }

        public static List<string> GetOptionalImportFieldList(string TableName, string path)
        {
            // Loading configuration file listing all data import columns
            var xml = XDocument.Load(path);

            // Query list of all columns for the type
            List<string> optFields = (from c in xml.Root.Descendants("Alias")
                                       .Where(i => i.Parent.Attribute("TableName").Value == TableName)
                                       .Where(j => j.Parent.Attribute("ReqInd").Value == "N")
                                      select c.Parent.Attribute("FieldName").Value
                                       ).ToList();
            return optFields;
        }

        public static V GetValueOrDefault<K, V>(this Dictionary<K, V> dict, K key)
        {
            dict.TryGetValue(key, out V ret);
            return ret;
        }


        public static string Encrypt(string value)
        {
            if (string.IsNullOrEmpty(value)) return value;
            try
            {
                var key = Encoding.UTF8.GetBytes("xn86wrnxSkQUNXlEolNmqGs0fXDsHXpt");

                using (var aesAlg = Aes.Create())
                {
                    using (var encryptor = aesAlg.CreateEncryptor(key, aesAlg.IV))
                    {
                        using (var msEncrypt = new MemoryStream())
                        {
                            using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                            using (var swEncrypt = new StreamWriter(csEncrypt))
                            {
                                swEncrypt.Write(value);
                            }

                            var iv = aesAlg.IV;

                            var decryptedContent = msEncrypt.ToArray();

                            var result = new byte[iv.Length + decryptedContent.Length];

                            Buffer.BlockCopy(iv, 0, result, 0, iv.Length);
                            Buffer.BlockCopy(decryptedContent, 0, result, iv.Length, decryptedContent.Length);

                            var str = Convert.ToBase64String(result);
                            var fullCipher = Convert.FromBase64String(str);
                            return str;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }

        public static string Decrypt(string value)
        {
            if (string.IsNullOrEmpty(value)) return value;
            try
            {
                value = value.Replace(" ", "+");
                var fullCipher = Convert.FromBase64String(value);

                var iv = new byte[16];
                var cipher = new byte[fullCipher.Length - iv.Length];

                Buffer.BlockCopy(fullCipher, 0, iv, 0, iv.Length);
                Buffer.BlockCopy(fullCipher, iv.Length, cipher, 0, fullCipher.Length - iv.Length);
                var key = Encoding.UTF8.GetBytes("xn86wrnxSkQUNXlEolNmqGs0fXDsHXpt");

                using (var aesAlg = Aes.Create())
                {
                    using (var decryptor = aesAlg.CreateDecryptor(key, iv))
                    {
                        string result;
                        using (var msDecrypt = new MemoryStream(cipher))
                        {
                            using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                            {
                                using (var srDecrypt = new StreamReader(csDecrypt))
                                {
                                    result = srDecrypt.ReadToEnd();
                                }
                            }
                        }

                        return result;
                    }
                }
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }
    }
}
