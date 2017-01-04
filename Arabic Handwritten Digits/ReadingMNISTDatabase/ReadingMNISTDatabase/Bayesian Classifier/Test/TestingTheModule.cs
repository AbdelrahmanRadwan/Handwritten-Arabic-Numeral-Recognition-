using ReadingMNISTDatabase.Bayesian_Classifier.Train;
using ReadingMNISTDatabase.Math_Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ReadingMNISTDatabase.Bayesian_Classifier.Test
{
    class TestingTheModule
    {
        BuildingTheModule  TheModule;
        MNIST_Database     TestData;
        public double []   Likelihood;
        public double []   Posterior;
        public double      Evidence;
        public int         TestingInstants;
        public int         NumberOfClasses;
        public int         NumberOfFeatures;
        public int    [,]  ConfusionMatrix;
        public double []   FeaturesDifferenceVector;
        double        []   Prior;
        public double      Accuracy;

        public TestingTheModule(MNIST_Database Data, BuildingTheModule Module)
        {
            this.TestData            = Data;
            this.TheModule           = Module;
            this.NumberOfClasses     = Module.NumberOfClasses;
            
            NumberOfFeatures         = Module.NumberOfFeatures;
            TestingInstants          = Data.m_pImagePatterns.Count();

            Likelihood               = new double [NumberOfClasses];
            ConfusionMatrix          = new int    [NumberOfClasses, NumberOfClasses];
            FeaturesDifferenceVector = new double [NumberOfFeatures];
            Prior                    = new double [NumberOfClasses];
            Posterior                = new double[NumberOfClasses];

        }
        public void FillPrior()
        {
            for (int i = 0; i < NumberOfClasses; i++)
            {
                Prior[i] =  TheModule.Instants[i] / TheModule.TrainingInstants;
               
            }
        }
        public void CalculateFeaturesDifferenceVector(int InstantIndex,int ClassIndex)
        {
            //Create the Features Difference Vector
            for (int i = 0; i < NumberOfFeatures; i++)
                FeaturesDifferenceVector[i] = TestData.m_pImagePatterns[InstantIndex].pPattern[i] - TheModule.FeaturesMean[ClassIndex, i];
        }
        public double px (double m , double s , double x)
        {
            return Math.Exp(-0.5 * Math.Pow((x - m), 2) / s) / Math.Sqrt(2 * Math.PI * s);
        }
        public void CalculateThelikelihoods(int InstantIndex)
        {

            for(int i=0;i<NumberOfClasses;i++)
            {
                //double temp;
                //temp= TheModule.CovarianceDeterminant[i];
                ////MessageBox.Show(TheModule.CovarianceDeterminant[i].ToString());
                //temp*= Math.Pow(2.0*Math.PI,TheModule.NumberOfActiveFeatures[i]/2.0);              
                //Likelihood[i] = temp;
                //CalculateFeaturesDifferenceVector(InstantIndex,i);
                //temp = -0.5 * MathOperations.VectorMatrixVectorMultiplication(FeaturesDifferenceVector, TheModule.Covariance,
                //                                                              FeaturesDifferenceVector,NumberOfFeatures, i);
                //Likelihood[i] = Math.Exp(temp)/Likelihood[i];
                double ans = 0;
                for (int j = 0; j < NumberOfFeatures; j++)
                {
                    if (TheModule.Covariance[i,j,j] != 0)
                    {
                        if (ans == 0)
                        {
                            ans = px(TheModule.FeaturesMean[i, j], TheModule.Covariance[i, j, j], TestData.m_pImagePatterns[InstantIndex].pPattern[j]);
                        }
                        else
                        {
                            ans *= px(TheModule.FeaturesMean[i, j], TheModule.Covariance[i, j, j], TestData.m_pImagePatterns[InstantIndex].pPattern[j]);
                        }
                    }
                }
                Likelihood[i] = ans;
            }
        }
        public void FillEvidence()
        {
            Evidence=0;
            for (int i = 0; i < NumberOfClasses; i++)
                Evidence += Likelihood[i] * Prior[i];
        }      
        public int FillPosterior()
        {
            int ClassifiedIndex=0;
            for(int i=0;i<NumberOfClasses;i++)
            {
                Posterior[i] = Likelihood[i] * Prior[i] / Evidence;
                if (Posterior[i] > Posterior[ClassifiedIndex])
                    ClassifiedIndex = i;
            }
            return ClassifiedIndex;
        }
        public void CalculateAccuracy()
        {
            Accuracy=0;
            for (int i = 0; i < NumberOfClasses; i++)
                Accuracy += ConfusionMatrix[i,i];
            Accuracy = Accuracy*100.0 / TestingInstants;
        }
        public void TestTheModule()
        {
            FillPrior();
            for (int i = 0; i < TestingInstants; i++)
            {
                
                CalculateThelikelihoods(i);
                FillEvidence();
                int classification=FillPosterior();
                ConfusionMatrix[TestData.m_pImagePatterns[i].nLabel, classification]++;
            }
            CalculateAccuracy();
        }
    }
}
