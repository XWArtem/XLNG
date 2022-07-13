using System;
using UnityEngine;

public class CSVReader : MonoBehaviour
{
    private WordsSpawner wordsSpawner;

    public TextAsset AllWords;  // 00
    public TextAsset BasicWords; // tag "Bazovye-slova-actual-final"  01
    public TextAsset Cara; // tag "Cara-parent-final"  02
    public TextAsset Cuerpo; // tag "Cuerpo-parent-final"  03
    public TextAsset SobreTiempo; // tag "Sobre-tiempo-parent-final"  04
    public TextAsset Colores; // tag "Colores-parent-final"  05
    public TextAsset Numericos; // tag "Numericos-parent-final"  06
    public TextAsset Comida_P1; // tag "Comida-P1-parent -final"  07
    public TextAsset Ropa; // tag "Ropa-parent-final"  08
    public TextAsset Casa; // tag "Casa-parent-final"  09
    public TextAsset Transporte; // tag "Transporte-parent-final" 10
    public TextAsset Profesiones; // tag "Profesiones-parent-final"  11
    public TextAsset Familia; //  tag  "Familia-parent-final"  12
    public TextAsset VidaCotidiana; // tag "Vida-cotidiana-parent-final"  13
    public TextAsset Cuestiones; // tag "Cuestiones-parent-final"   14



    public int WordsAmount;


    // these values we use in WordsSpawner script:
    public string[] TableName = new string[15];
    public int TablesAmount;
    public int[] tableSize;

    [System.Serializable]
    public class Pair
    {
        public string rusWord;
        public string espWord;
    }

    [System.Serializable]
    public class WordsList
    {
        public Pair[] PairOfWords;
    }

    public WordsList allWordsList = new WordsList();
    public WordsList basicWordsList = new WordsList();
    public WordsList caraList = new WordsList();
    public WordsList cuerpoList = new WordsList();
    public WordsList sobreTiempoList = new WordsList();
    public WordsList coloresList = new WordsList();
    public WordsList numericosList = new WordsList();
    public WordsList comidaP1List = new WordsList();
    public WordsList ropaList = new WordsList();
    public WordsList casaList = new WordsList();
    public WordsList transporteList = new WordsList();
    public WordsList profesionesList = new WordsList();
    public WordsList familiaList = new WordsList();
    public WordsList vidaCotidianaList = new WordsList();
    public WordsList cuestionesList = new WordsList();

    private void Awake()
    {
        tableSize = new int[15];
        wordsSpawner = FindObjectOfType<WordsSpawner>();
        TablesAmount = 14;
        //ReadCSV(AllWords, allWordsList, 0);
        ReadCSV(BasicWords, basicWordsList, 1);
        ReadCSV(Cara, caraList, 2);
        ReadCSV(Cuerpo, cuerpoList, 3);
        ReadCSV(SobreTiempo, sobreTiempoList, 4);
        ReadCSV(Colores, coloresList, 5);
        ReadCSV(Numericos, numericosList, 6);
        ReadCSV(Comida_P1, comidaP1List, 7);
        ReadCSV(Ropa, ropaList, 8);
        ReadCSV(Casa, casaList, 9);
        ReadCSV(Transporte, transporteList, 10);
        ReadCSV(Profesiones, profesionesList, 11);
        ReadCSV(Familia, familiaList, 12);
        ReadCSV(VidaCotidiana, vidaCotidianaList, 13);
        ReadCSV(Cuestiones, cuestionesList, 14);
    }

    void ReadCSV(TextAsset textAsset, WordsList wordsList, int tableIndex)
    {
        Debug.Log("Table index is: " + tableIndex);
        string[] data = textAsset.text.Split(new string[] { ";", "\n" }, StringSplitOptions.None);

        tableSize[tableIndex] = data.Length / 4;
        wordsList.PairOfWords = new Pair[tableSize[tableIndex]];

        for (int i = 0; i < tableSize[tableIndex]; i++)
        {
            wordsList.PairOfWords[i] = new Pair();
            wordsList.PairOfWords[i].rusWord = data[4 * i + 2];
            wordsList.PairOfWords[i].espWord = data[4 * i];
            TableName[tableIndex] = textAsset.name;
        }
    }



    public string SetRusWord(int index, string tablename)
    {
        if (tablename == "Bazovye-slova-actual-final")
        {
            return basicWordsList.PairOfWords[index].rusWord;
        }
        else if (tablename == "Cara-parent-final")
        {
            return caraList.PairOfWords[index].rusWord;
        }
        else if (tablename == "Cuerpo-parent-final")
        {
            return cuerpoList.PairOfWords[index].rusWord;
        }
        else if (tablename == "Sobre-tiempo-parent-final")
        {
            return sobreTiempoList.PairOfWords[index].rusWord;
        }
        else if (tablename == "Colores-parent-final")
        {
            return coloresList.PairOfWords[index].rusWord;
        }
        else if (tablename == "Numericos-parent-final")
        {
            return numericosList.PairOfWords[index].rusWord;
        }
        else if (tablename == "Comida-P1-parent -final")
        {
            return comidaP1List.PairOfWords[index].rusWord;
        }
        else if (tablename == "Ropa-parent-final")
        {
            return ropaList.PairOfWords[index].rusWord;
        }
        else if (tablename == "Casa-parent-final")
        {
            return casaList.PairOfWords[index].rusWord;
        }
        else if (tablename == "Transporte-parent-final")
        {
            return transporteList.PairOfWords[index].rusWord;
        }
        else if (tablename == "Profesiones-parent-final")
        {
            return profesionesList.PairOfWords[index].rusWord;
        }
        else if (tablename == "Familia-parent-final")
        {
            return familiaList.PairOfWords[index].rusWord;
        }
        else if (tablename == "Vida-cotidiana-parent-final")
        {
            return vidaCotidianaList.PairOfWords[index].rusWord;
        }
        else if (tablename == "Cuestiones-parent-final")
        {
            return cuestionesList.PairOfWords[index].rusWord;
        }
        else return basicWordsList.PairOfWords[index].rusWord;
    }
    public string SetEspWord(int index, string tablename)
    {
        if (tablename == "Bazovye-slova-actual-final")
        {
            return basicWordsList.PairOfWords[index].espWord;
        }
        else if (tablename == "Cara-parent-final")
        {
            return caraList.PairOfWords[index].espWord;
        }
        else if (tablename == "Cuerpo-parent-final")
        {
            return cuerpoList.PairOfWords[index].espWord;
        }
        else if (tablename == "Sobre-tiempo-parent-final")
        {
            return sobreTiempoList.PairOfWords[index].espWord;
        }
        else if (tablename == "Colores-parent-final")
        {
            return coloresList.PairOfWords[index].espWord;
        }
        else if (tablename == "Numericos-parent-final")
        {
            return numericosList.PairOfWords[index].espWord;
        }
        else if (tablename == "Comida-P1-parent -final")
        {
            return comidaP1List.PairOfWords[index].espWord;
        }
        else if (tablename == "Ropa-parent-final")
        {
            return ropaList.PairOfWords[index].espWord;
        }
        else if (tablename == "Casa-parent-final")
        {
            return casaList.PairOfWords[index].espWord;
        }
        else if (tablename == "Transporte-parent-final")
        {
            return transporteList.PairOfWords[index].espWord;
        }
        else if (tablename == "Profesiones-parent-final")
        {
            return profesionesList.PairOfWords[index].espWord;
        }
        else if (tablename == "Familia-parent-final")
        {
            return familiaList.PairOfWords[index].espWord;
        }
        else if (tablename == "Vida-cotidiana-parent-final")
        {
            return vidaCotidianaList.PairOfWords[index].espWord;
        }
        else if (tablename == "Cuestiones-parent-final")
        {
            return cuestionesList.PairOfWords[index].espWord;
        }
        else
        {
            return caraList.PairOfWords[index].espWord;
        }

            
    }
}
