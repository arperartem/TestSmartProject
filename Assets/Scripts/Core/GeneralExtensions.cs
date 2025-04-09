using System.Linq;
using Core.Data;

namespace Core
{
    public static class GeneralExtensions
    {
        public static BoosterType[] GetRandomBoosters(this BoosterType[] list, int amount, int amountUniq, BoosterType[] lastSet)
        {
            if(list.Length < amountUniq * 2 || amountUniq > amount || amount > list.Length)
                throw new System.ArgumentException("Invalid amount of boosters");

            var result = new BoosterType[amount];
            var shakeArray = ShakeArray(list);

            var amountUniqApply = 0;
            var amountApply = 0;
            var index = 0;
            while (amountUniqApply + amountApply != amount)
            {
                if (amountUniqApply <= amountUniq && lastSet.Contains(shakeArray[index]) == false)
                {
                    result[amountApply + amountUniqApply] = shakeArray[index];
                    amountUniqApply++;
                }
                else if(amountApply < amount - amountUniq)
                {
                    result[amountApply + amountUniqApply] = shakeArray[index];
                    amountApply++;
                }
                
                index++;
            }

            return result;
        }
        
        private static T[] ShakeArray<T>(T[] array)
        {
            var newArray = array.ToArray();
            
            for (var i = newArray.Length - 1; i > 0; i--)
            {
                var j =  UnityEngine.Random.Range(0, i + 1);
                var temp = newArray[i];
                newArray[i] = newArray[j];
                newArray[j] = temp;
            }

            return newArray;
        }
    }
}