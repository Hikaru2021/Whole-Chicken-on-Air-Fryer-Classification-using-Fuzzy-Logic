using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class FuzzySet
{
    FuzzyLogic accessor;
    public double classification_COA = 0.0;
    public double membershipOfweight = 0.0;

    public FuzzyLogic[] Time = new FuzzyLogic[3];
    public FuzzyLogic[] Temperature = new FuzzyLogic[3];
    public FuzzyLogic[] classification = new FuzzyLogic[3];

    //input variables Time
    private const int SHORT = 0;
    private const int AVERAGE = 1;
    private const int LONG = 2;

    //input variables Temp
    private const int LOW = 0;
    private const int MEDIUM = 1;
    private const int HIGH = 2;

    //output
    private const int UNDERCOOKED = 0;
    private const int WELLCOOKED = 1;
    private const int OVERCOOKED = 2;

    int[,] matched_features = new int[3, 3];

    public void InitializeFuzzySets()
    {
        //input variables
        for (int i = 0; i < 3; i++)
        {
            Time[i] = new FuzzyLogic();
            Temperature[i] = new FuzzyLogic();
        }

        //output variable
        for (int i = 0; i < 3; i++)
        {
            classification[i] = new FuzzyLogic();
        }

        //Membership function for Time
        Time[0].Set("SHORT", 0, 1, 0, 15, 1, 15, 1, 25, 0);
        Time[1].Set("AVERAGE", 1, 19, 0, 27, 1, 27, 1, 35, 0);
        Time[2].Set("LONG", 2, 29, 0, 45, 1, 45, 1, 61, 0);

        //Membership function for Temperature
        Temperature[0].Set("LOW", 0, 349, 0, 365, 1, 365, 1, 380, 0);
        Temperature[1].Set("MEDIUM", 1, 370, 0, 385, 1, 385, 1, 400, 0);
        Temperature[2].Set("HIGH", 2, 390, 0, 415, 1, 415, 1, 451, 0);


        classification[0].Set("UNDER-COOKED", 0, 0, 1, 17.5, 1, 17.5, 1, 35, 0);
        classification[1].Set("WELL-COOKED", 1, 30, 0, 47.5, 1, 47.5, 1, 65, 0);
        classification[2].Set("OVER-COOKED", 2, 60, 0, 75, 1, 75, 1, 100, 0);

        //Fuzzy Rules
        matched_features[0, 0] = UNDERCOOKED;
        matched_features[0, 1] = UNDERCOOKED;
        matched_features[0, 2] = OVERCOOKED;

        matched_features[1, 0] = WELLCOOKED;
        matched_features[1, 1] = WELLCOOKED;
        matched_features[1, 2] = OVERCOOKED;


        matched_features[2, 0] = WELLCOOKED;
        matched_features[2, 1] = OVERCOOKED;
        matched_features[2, 2] = OVERCOOKED;

    }

    public double computeCentroid(double time, double temp)
    {
        accessor = new FuzzyLogic();
        int i = 0, j = 0;
        double area = 0, centroid = 0, numerator = 0, denominator = 0, minimum = 0.0, centerOfArea = 0.0;

        for (i = 0; i < 3; i++) // time input
            for (j = 0; j < 3; j++) // temp input
            {
                minimum = accessor.min(Time[i].membership(time), Temperature[j].membership(temp));
                if (minimum != 0)
                {
                    area = classification[matched_features[Time[i].GetIndex(), Temperature[j].GetIndex()]].Area(minimum);
                    centroid = classification[matched_features[Time[i].GetIndex(), Temperature[j].GetIndex()]].CenterOfArea(minimum);
                    numerator += area * centroid;
                    denominator += area;
                }
            }

        centerOfArea = numerator / denominator;

        if (denominator == 0.0)
            return 0.0;
        else
            return centerOfArea;
    }

    //Get the membership inference value
    public string defuzzify(double time, double temp)
    {

        InitializeFuzzySets();
        classification_COA = computeCentroid(time, temp);
        double[] membershipArray = new double[3];

        double maxMembership = membershipArray[0];
        string classification_linguistic = "";
        for (int i = 0; i < 3; i++)
        {
            if ((classification[i].membership(classification_COA)) > maxMembership)
            {
                maxMembership = classification[i].membership(classification_COA);
                classification_linguistic = classification[i].GetLinguistic();
            }
        }
        return classification_linguistic;
    }
}

