using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backgammon;
using Backgammon.enumsFolder;
using Backgammon.StepsFolder;

namespace Backgammon.GameValidations
{
    public class CubeStepExistValidator
    {
        public static bool Validate(int slotIdSource, int slotIdDestination, IPlayer blackPlayer,IPlayer whitePlayer, Turn turn, List<Step> Steps)
        {
            IPlayer player;
            if (turn == Turn.Black)
            {
                player = blackPlayer;
            }
            else // white
            {
                player = whitePlayer;
            }
            switch (player.PlayerState)
            {
                case PlayerState.Normal:
                  return  CubeStepExistNormal.Validate(slotIdSource, slotIdDestination, turn, Steps);
                case PlayerState.TakingOut:
                    return CubeStepExistTakinIn.Validate(slotIdSource, slotIdDestination, turn, Steps);
                case PlayerState.TakingIn:
                    return CubeStepExistTakingOut.Validate(slotIdSource, slotIdDestination, turn, Steps);
                case PlayerState.Blocked:
                    return false;
                default:
                    throw new Exception("somthing wrong with the CubesStepExistValidators");
            }
        }

    }
    //public class CubeStepExistValidatorWhitePlayer
    //{
    //    public static bool Vlidate(int slotIdSource, int slotIdDestination, IPlayer player, List<Step> Steps)
    //    {
    //        switch (player.PlayerState)
    //        {
    //            case PlayerState.Normal:

    //            case PlayerState.TakingOut:

    //            case PlayerState.TakingIn:

    //            case PlayerState.Blocked:
    //                return false;
    //            default:
    //                throw new Exception("somthing wrong with the CubesStepExistValidators");
    //        }
    //    }
    //}
    //public class CubeStepExistValidatorBlackPlayer
    //{
    //    public static bool Validate(int slotIdSource, int slotIdDestination, IPlayer player, List<Step> Steps)
    //    {
    //        switch (player.PlayerState)
    //        {
    //            case PlayerState.Normal:

    //            case PlayerState.TakingOut:

    //            case PlayerState.TakingIn:

    //            case PlayerState.Blocked:
    //                return false;
    //            default:
    //                throw new Exception("somthing wrong with the CubesStepExistValidators");
    //        }
    //    }
    //}
    ////white validators:
    //public class CubeStepExistWhiteNormal
    //{
    //    public static bool Validate(int slotIdSource, int slotIdDestination, List<Step> Steps)
    //    {
    //        var playerStep = StepsConverter.ConvertToStep(slotIdSource)
    //        foreach (var step in Steps)
    //        {
    //            if (step.Value == move)
    //            {
    //                return true;
    //            }
    //        }
    //        return false;
    //    }
    //}
    //public class CubeStepExistWhiteTakingOut
    //{
    //    public static bool Validate(int slotIdSource, int slotIdDestination, List<Step> Steps)
    //    {
    //        var move = slotIdDestination - slotIdSource;
    //        foreach (var step in Steps)
    //        {
    //            if (step.Value == move || step.Value > move)
    //            {
    //                return true;
    //            }
    //        }
    //        return false;
    //    }
    //}
    //public class CubeStepExistWhiteTakingIn
    //{
    //    public static bool Validate(int slotIdSource, int slotIdDestination, List<Step> Steps)
    //    {
    //        if (slotIdSource != int.MinValue)
    //        {
    //            return false;
    //        }
    //        //convert the int.Minvalue:
    //        slotIdSource = 0;
    //        var move = slotIdDestination - slotIdSource;
    //        foreach (var step in Steps)
    //        {
    //            if (step.Value == move || step.Value > move)
    //            {
    //                return true;
    //            }
    //        }
    //        return false;
    //    }
    //}
    ////black validators:
    //public class CubeStepExistBlackNormal
    //{
    //    public static bool Validate(int slotIdSource, int slotIdDestination, List<Step> Steps)
    //    {
    //        var move = slotIdSource - slotIdDestination;
    //        foreach (var step in Steps)
    //        {
    //            if (step.Value == move)
    //            {
    //                return true;
    //            }
    //        }
    //        return false;
    //    }
    //}
    //public class CubeStepExistBlackTakingOut
    //{
    //    public static bool Validate(int slotIdSource, int slotIdDestination, List<Step> Steps)
    //    {
    //        var move = slotIdSource - slotIdDestination;
    //        foreach (var step in Steps)
    //        {
    //            if (step.Value == move || step.Value > move)
    //            {
    //                return true;
    //            }
    //        }
    //        return false;
    //    }
    //}
    //---------------------------
    public class CubeStepExistNormal
    {
        public static bool Validate(int slotIdSource, int slotIdDestination, Turn turn, List<Step> Steps)
        {
            var playerStep = StepsConverter.ConvertToStep(slotIdSource, slotIdDestination, turn);
            foreach (var step in Steps)
            {
                if(step.Value == playerStep.Value)
                {
                    return true;
                }
            }
            return false;
        }
    }
    public class CubeStepExistTakinIn
    {
        public static bool Validate(int slotIdSource, int slotIdDestination, Turn turn, List<Step> Steps)
        {
            return CubeStepExistNormal.Validate(slotIdSource, slotIdDestination, turn, Steps);
        }
    }
    public class CubeStepExistTakingOut
    {
        public static bool Validate(int slotIdSource, int slotIdDestination, Turn turn, List<Step> Steps)
        {
            var playerStep = StepsConverter.ConvertToStep(slotIdSource, slotIdDestination, turn);
            foreach (var step in Steps)
            {
                if (step.Value == playerStep.Value || step.Value > playerStep.Value)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
