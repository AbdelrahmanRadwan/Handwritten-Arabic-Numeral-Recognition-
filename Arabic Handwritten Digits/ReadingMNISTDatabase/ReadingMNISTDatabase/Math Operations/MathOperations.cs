using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReadingMNISTDatabase.Math_Operations
{
    static public class MathOperations
    {
        static public double VectorMatrixVectorMultiplication(double[] vector1, double[,,] matrix, double[] vector3, int NumberOfFeatures, int ClassIndex)
        {
            double[] vector2 = new double[NumberOfFeatures];
            double answer = 0;
            for (int column = 0; column < NumberOfFeatures; column++)
            {
                vector2[column] =vector1[column] * matrix[ClassIndex, column, column];
            }

            for (int i = 0; i < NumberOfFeatures; i++)
                answer = answer + vector2[i] * vector3[i];
                return answer;
        }
    }
}
