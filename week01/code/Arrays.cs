public static class Arrays
{
    /// <summary>
    /// This function will produce an array of size 'length' starting with 'number' followed by multiples of 'number'.  For 
    /// example, MultiplesOf(7, 5) will result in: {7, 14, 21, 28, 35}.  Assume that length is a positive
    /// integer greater than 0.
    /// </summary>
    /// <returns>array of doubles that are the multiples of the supplied number</returns>
    public static double[] MultiplesOf(double number, int length)
    {
        // TODO Problem 1 Start

        // Plan (step-by-step):
        // 1. Create an array of doubles with size 'length'.
        // 2. For each index i from 0 to length-1:
        //    a. Compute the (i+1)-th multiple of 'number' -> number * (i + 1)
        //    b. Store that value into the array at index i.
        // 3. Return the filled array.
        //
        // This produces: MultiplesOf(3,5) => {3, 6, 9, 12, 15}

        double[] result = new double[length];

        for (int i = 0; i < length; i++)
        {
            result[i] = number * (i + 1);
        }

        return result;
    }

    /// <summary>
    /// Rotate the 'data' to the right by the 'amount'.  For example, if the data is 
    /// List<int>{1, 2, 3, 4, 5, 6, 7, 8, 9} and an amount is 3 then the list after the function runs should be 
    /// List<int>{7, 8, 9, 1, 2, 3, 4, 5, 6}.  The value of amount will be in the range of 1 to data.Count, inclusive.
    ///
    /// Because a list is dynamic, this function will modify the existing data list rather than returning a new list.
    /// </summary>
    public static void RotateListRight(List<int> data, int amount)
    {
        // TODO Problem 2 Start

        // Plan (step-by-step):
        // 1. Validate assumptions (the problem states amount is between 1 and data.Count inclusive).
        //    If data is null or has 0/1 elements, nothing needs to be done.
        // 2. Make a shallow copy of the original list (so we can read original ordering while we modify the original).
        // 3. Determine the slice of elements that will move to the front:
        //    - These are the last 'amount' elements of the original list.
        // 4. Determine the remaining elements:
        //    - These are the first data.Count - amount elements.
        // 5. Clear the original list and AddRange the two slices in the new order:
        //    - first add the last-'amount' slice, then add the remaining front slice.


        if (data == null) return;
        int n = data.Count;
        if (n <= 1) return;

        if (amount <= 0) return;
        if (amount >= n)
        {
            amount %= n;
            if (amount == 0) return;
        }

        List<int> original = new List<int>(data);

        List<int> tail = original.GetRange(n - amount, amount);

        List<int> head = original.GetRange(0, n - amount);


        data.Clear();
        data.AddRange(tail);
        data.AddRange(head);

    }
}
