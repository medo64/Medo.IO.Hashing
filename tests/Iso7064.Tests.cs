using System;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Medo.IO.Hashing;

namespace Tests;

[TestClass]
public class Iso7064_Tests {

    [TestMethod]
    public void Iso7064_SingleDigit1() {
        var crc = new Iso7064();
        crc.Append(Encoding.ASCII.GetBytes("0"));
        Assert.AreEqual(2, crc.HashAsNumber);
        Assert.AreEqual("32", BitConverter.ToString(crc.GetCurrentHash()).Replace("-", ""));
    }

    [TestMethod]
    public void Iso7064_SingleDigit2() {
        var crc = new Iso7064();
        crc.Append(Encoding.ASCII.GetBytes("1"));
        Assert.AreEqual(9, crc.HashAsNumber);
        Assert.AreEqual("39", BitConverter.ToString(crc.GetCurrentHash()).Replace("-", ""));
    }

    [TestMethod]
    public void Iso7064_SingleDigit3() {
        var crc = new Iso7064();
        crc.Append(Encoding.ASCII.GetBytes("6"));
        Assert.AreEqual(0, crc.HashAsNumber);
        Assert.AreEqual("30", BitConverter.ToString(crc.GetCurrentHash()).Replace("-", ""));
    }

    [TestMethod]
    public void Iso7064_SingleDigit4() {
        var crc = new Iso7064();
        crc.Append(Encoding.ASCII.GetBytes("9"));
        Assert.AreEqual(4, crc.HashAsNumber);
        Assert.AreEqual("34", BitConverter.ToString(crc.GetCurrentHash()).Replace("-", ""));
    }

    [TestMethod]
    public void Iso7064_Numbers1() {
        var crc = new Iso7064();
        crc.Append(Encoding.ASCII.GetBytes("0823"));
        Assert.AreEqual(5, crc.HashAsNumber);
        Assert.AreEqual("35", BitConverter.ToString(crc.GetCurrentHash()).Replace("-", ""));
    }

    [TestMethod]
    public void Iso7064_Numbers2() {
        var crc = new Iso7064();
        crc.Append(Encoding.ASCII.GetBytes("276616973212561"));
        Assert.AreEqual(5, crc.HashAsNumber);
        Assert.AreEqual("35", BitConverter.ToString(crc.GetCurrentHash()).Replace("-", ""));
    }

    [TestMethod]
    public void Iso7064_Numbers3() {
        var crc = new Iso7064();
        crc.Append(Encoding.ASCII.GetBytes("65"));
        Assert.AreEqual(0, crc.HashAsNumber);
        Assert.AreEqual("30", BitConverter.ToString(crc.GetCurrentHash()).Replace("-", ""));
    }

    [TestMethod]
    public void Iso7064_Numbers4() {
        var crc = new Iso7064();
        crc.Append(Encoding.ASCII.GetBytes("56"));
        Assert.AreEqual(0, crc.HashAsNumber);
        Assert.AreEqual("30", BitConverter.ToString(crc.GetCurrentHash()).Replace("-", ""));
    }

    [TestMethod]
    public void Iso7064_Numbers5() {
        var crc = new Iso7064();
        crc.Append(Encoding.ASCII.GetBytes("732"));
        Assert.AreEqual(5, crc.HashAsNumber);
        Assert.AreEqual("35", BitConverter.ToString(crc.GetCurrentHash()).Replace("-", ""));
    }

    [TestMethod]
    public void Iso7064_Numbers6() {
        var crc = new Iso7064();
        crc.Append(Encoding.ASCII.GetBytes("723"));
        Assert.AreEqual(5, crc.HashAsNumber);
        Assert.AreEqual("35", BitConverter.ToString(crc.GetCurrentHash()).Replace("-", ""));
    }

    [TestMethod]
    public void Iso7064_Numbers7() {
        var crc = new Iso7064();
        crc.Append(Encoding.ASCII.GetBytes("8373426074"));
        Assert.AreEqual(9, crc.HashAsNumber);
        Assert.AreEqual("39", BitConverter.ToString(crc.GetCurrentHash()).Replace("-", ""));
    }

    [TestMethod]
    public void Iso7064_Numbers8() {
        var crc = new Iso7064();
        crc.Append(Encoding.ASCII.GetBytes("4428922675"));
        Assert.AreEqual(7, crc.HashAsNumber);
        Assert.AreEqual("37", BitConverter.ToString(crc.GetCurrentHash()).Replace("-", ""));
    }

    [TestMethod]
    public void Iso7064_Reuse() {
        var crc = new Iso7064();
        crc.Append(Encoding.ASCII.GetBytes("4428922675"));
        Assert.AreEqual(7, crc.HashAsNumber);
        Assert.AreEqual("37", BitConverter.ToString(crc.GetHashAndReset()).Replace("-", ""));
        crc.Append(Encoding.ASCII.GetBytes("4428922675"));
        Assert.AreEqual(7, crc.HashAsNumber);
        Assert.AreEqual("37", BitConverter.ToString(crc.GetCurrentHash()).Replace("-", ""));
    }

    [TestMethod]
    public void Iso7064_Validate() {
        Assert.AreEqual("7", Iso7064.ComputeHash("4428922675"));
        Assert.IsTrue(Iso7064.ValidateHash("44289226757"));
    }

    [TestMethod]
    public void Iso7064_HelperComputeHash() {
        var hash = Iso7064.ComputeHash("572");
        Assert.AreEqual("1", hash);
    }

    [TestMethod]
    public void Iso7064_HelperComputeHashSpaces() {
        var hash = Iso7064.ComputeHash(" 5 7 2 ");
        Assert.AreEqual("1", hash);
    }

    [TestMethod]
    public void Iso7064_HelperComputeHashDashes() {
        var hash = Iso7064.ComputeHash("05-72");
        Assert.AreEqual("7", hash);
    }


    [TestMethod]
    public void Iso7064_HelperComputeHashFull() {
        var hash = Iso7064.ComputeHash("572", returnAllDigits: true);
        Assert.AreEqual("5721", hash);
    }

    [TestMethod]
    public void Iso7064_HelperComputeHashSpacesFull() {
        var hash = Iso7064.ComputeHash(" 5 7 2 ", returnAllDigits: true);
        Assert.AreEqual("5721", hash);
    }

    [TestMethod]
    public void Iso7064_HelperComputeHashDashesFull() {
        var hash = Iso7064.ComputeHash("05-72", returnAllDigits: true);
        Assert.AreEqual("05727", hash);
    }


    [TestMethod]
    public void Iso7064_HelperValidateHash() {
        var result = Iso7064.ValidateHash("5721");
        Assert.IsTrue(result);
    }

    [TestMethod]
    public void Iso7064_HelperValidateHashSpaces() {
        var result = Iso7064.ValidateHash(" 57 21 ");
        Assert.IsTrue(result);
    }

    [TestMethod]
    public void Iso7064_HelperValidateHashDashes() {
        var result = Iso7064.ValidateHash("-57-21-");
        Assert.IsTrue(result);
    }


    [TestMethod]
    public void Iso7064_HelperValidateHashFails() {
        var result = Iso7064.ValidateHash("5720");
        Assert.IsFalse(result);
    }


    [TestMethod]
    public void Iso7064_InvalidCharacters572() {
        var checksum = new Iso7064();
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => {
            checksum.Append(new byte[] { 5, 7, 2 });
        });
    }

    [TestMethod]
    public void Iso7064_InvalidCharactersABC() {
        var checksum = new Iso7064();
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => {
            checksum.Append(Encoding.UTF8.GetBytes("ABC"));
        });
    }

}
