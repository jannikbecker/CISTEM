using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Cistem.Test
{
  [TestClass]
  public class CistemTest
  {
    [TestMethod]
    public async Task CistemTest_Validate()
    {
      var perl = await File.ReadAllLinesAsync("perl.txt");
      var words = await File.ReadAllLinesAsync("de_wordlist_simple.txt");

      Assert.AreEqual(perl.Length, words.Length);

      for (int i = 0; i < words.Length; i++)
      {
        var word = words[i];

        var line = new StringBuilder();
        line.Append(word);
        line.Append('\t');
        line.Append(Cistem.Stem(word));
        line.Append('\t');
        line.Append(Cistem.Stem(word, true));
        line.Append('\t');

        var segment = Cistem.Segment(word);

        line.Append(segment[0]);
        line.Append('\t');
        line.Append(segment[1]);
        line.Append('\t');

        segment = Cistem.Segment(word, true);
        line.Append(segment[0]);
        line.Append('\t');
        line.Append(segment[1]);

        var expected = perl[i];
        var actual = line.ToString();
        Assert.AreEqual(expected, actual);
      }
    }
  }
}
