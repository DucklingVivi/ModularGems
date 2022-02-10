using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Mono.Cecil.Cil;
using MonoMod.Cil;
using Terraria;

namespace ModularGems.ILEdits
{
    internal class BreathingPatch : ILEdit
    {
        public override void Apply()
        {
            IL.Terraria.Player.CheckDrowning += Player_CheckDrowning;
        }

        private void Player_CheckDrowning(ILContext il)
        {
            //ModularGems.Instance.Logger.Debug(il.ToString());
            var c = new ILCursor(il);

            c.GotoNext(
                x => x.MatchLdcI4(0),
                x => x.MatchCall(typeof(Terraria.Collision), nameof(Terraria.Collision.DrownCollision)),
                x => x.MatchStloc(0)
                );
            c.Index += 3;
            c.Emit(OpCodes.Ldarg_0);
            c.Emit(OpCodes.Ldloc_0);
            c.EmitDelegate<Func<Player, bool, bool>>((player, i) => {

                bool flag = false;
                if(player.whoAmI == Main.myPlayer)
                {
                    flag = player.GetModPlayer<ModularGemsPlayer>().isFish;
                }
                if (flag)
                {
                    return !i;
                }
                return i;
            });
            c.Emit(OpCodes.Stloc,0);
        }
    }
}
