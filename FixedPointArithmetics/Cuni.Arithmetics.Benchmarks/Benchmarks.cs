using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenchmarkDotNet.Running;
using Cuni.Arithmetics.FixedPoint;

namespace Cuni.Arithmetics.Benchmarks
{
    public class Benchmarks
    {

        static class MatrixOperations<Q> where Q : QType
        {
            public static Fixed<Q>[,] MatrixMult(Fixed<Q>[,] A, Fixed<Q>[,] B)
            {
                Fixed<Q>[,] matrix = new Fixed<Q>[A.GetLength(0), B.GetLength(1)];
                for (int row = 0; row < matrix.GetLength(0); row++)
                {
                    for (int col = 0; col < matrix.GetLength(1); col++)
                    {
                        for (int i = 0; i < A.GetLength(1); i++)
                        {
                            matrix[row, col] += A[row, i] * B[i, col];
                        }
                    }
                }
                return matrix;
            }

            public static Fixed<Q>[,] AddCol(Fixed<Q>[,] A, Fixed<Q>[,] b)
            {
                Fixed<Q>[,] matrix = new Fixed<Q>[A.GetLength(0), A.GetLength(1) + 1];
                for (int row = 0; row < matrix.GetLength(0); row++)
                {
                    for (int col = 0; col < matrix.GetLength(1); col++)
                    {
                        if (col < A.GetLength(1))
                        {
                            matrix[row, col] = A[row, col];
                        }
                        else
                        {
                            matrix[row, col] = b[row, 0];
                        }
                    }
                }
                return matrix;
            }

            static void InterchangeRow(int iRow1, int iRow2, ref Fixed<Q>[,] reducedMatrix)
            {
                for (int j = 0; j < reducedMatrix.GetLength(1); j++)
                {
                    Fixed<Q> temp = reducedMatrix[iRow1, j];
                    reducedMatrix[iRow1, j] = reducedMatrix[iRow2, j];
                    reducedMatrix[iRow2, j] = temp;
                }
            }

            static void MultiplyRow(int iRow, Fixed<Q> num, ref Fixed<Q>[,] reducedMatrix)
            {
                for (int j = 0; j < reducedMatrix.GetLength(1); j++)
                {
                    reducedMatrix[iRow, j] *= num;
                }
            }

            static void AddRow(int iTargetRow, int iSecondRow, Fixed<Q> iMultiple, ref Fixed<Q>[,] reducedMatrix)
            {
                for (int j = 0; j < reducedMatrix.GetLength(1); j++)
                    reducedMatrix[iTargetRow, j] += (reducedMatrix[iSecondRow, j] * iMultiple);
            }

            public static Fixed<Q>[,] ReducedEchelonForm(Fixed<Q>[,] M)
            {
                Fixed<Q>[,] reducedMatrix = M;
                for (int i = 0; i < M.GetLength(0); i++)
                {
                    if (reducedMatrix[i, i] == 0)
                        for (int j = i + 1; j < reducedMatrix.GetLength(0); j++)
                            if (reducedMatrix[j, i] != 0)
                                InterchangeRow(i, j, ref reducedMatrix);
                    if (reducedMatrix[i, i] == 0)
                        continue;
                    if (reducedMatrix[i, i] != 1)
                        for (int j = i + 1; j < reducedMatrix.GetLength(0); j++)
                            if (reducedMatrix[j, i] == 1)
                                InterchangeRow(i, j, ref reducedMatrix);
                    MultiplyRow(i, 1 / reducedMatrix[i, i], ref reducedMatrix);
                    for (int j = i + 1; j < reducedMatrix.GetLength(0); j++)
                        AddRow(j, i, -reducedMatrix[j, i], ref reducedMatrix);
                    for (int j = i - 1; j >= 0; j--)
                        AddRow(j, i, -reducedMatrix[j, i], ref reducedMatrix);
                }
                return reducedMatrix;
            }
        }

        static Fixed<Q24_8> q24 = 10;
        static Fixed<Q16_16> q16 = 10;
        static Fixed<Q8_24> q8 = 10;

        private static Fixed<Q24_8>[,] q24_Matrix =
        {
            {q24,   q24*2,  q24/2 },
            {q24*3, q24+8,  q24*5 },
            {q24,   q24+7,  q24*2 }
        };

        private static Fixed<Q16_16>[,] q16_Matrix =
        {
            {q16,   q16*2,  q16/2 },
            {q16*3, q16+8,  q16*5 },
            {q16,   q16+7,  q16*2 }
        };
        private static Fixed<Q8_24>[,] q8_Matrix =
        {
            {q8,   q8*2,  q8/2 },
            {q8*3, q8+8,  q8*5 },
            {q8,   q8+7,  q8*2 }
        };

        [ClrJob(baseline: true)]
        public class FixedOperationsTests
        {

            [Benchmark]
            public Fixed<Q24_8> AddQ24()
            {
                Fixed<Q24_8> var = 378791283 + 187;
                return var.Add(q24);
            }

            [Benchmark]
            public Fixed<Q16_16> AddQ16()
            {
                Fixed<Q16_16> var = 21784 + 187;
                return var.Add(q16);
            }

            [Benchmark]
            public Fixed<Q8_24> AddQ8()
            {
                Fixed<Q8_24> var = 5 + 187;
                return var.Add(q8);
            }

            [Benchmark]
            public Fixed<Q24_8> MultQ24()
            {
                return q24.Multiply(q24);
            }

            [Benchmark]
            public Fixed<Q16_16> MultQ16()
            {
                return q16.Multiply(q16);
            }

            [Benchmark]
            public Fixed<Q8_24> MultQ8()
            {
                return q8.Multiply(q8);
            }

            [Benchmark]
            public Fixed<Q24_8>[,] Q24_GaussElimination()
            {
                return MatrixOperations<Q24_8>.ReducedEchelonForm(q24_Matrix);
            }

            [Benchmark]
            public Fixed<Q16_16>[,] Q16_GaussElimination()
            {
                return MatrixOperations<Q16_16>.ReducedEchelonForm(q16_Matrix);
            }

            [Benchmark]
            public Fixed<Q8_24>[,] Q8_GaussElimination()
            {
                return MatrixOperations<Q8_24>.ReducedEchelonForm(q8_Matrix);
            }
        }


        static void Main(string[] args)
        {
            BenchmarkRunner.Run<FixedOperationsTests>();
            
        }
    }
}
