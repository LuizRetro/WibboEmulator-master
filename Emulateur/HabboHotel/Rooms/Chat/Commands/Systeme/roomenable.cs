using Butterfly.HabboHotel.GameClients;

            if (!ButterflyEnvironment.GetGame().GetEffectsInventoryManager().EffectExist(NumEnable, Session.GetHabbo().HasFuse("fuse_mod")))
                return;
            
                if (!User.IsBot)
                {
                    User.ApplyEffect(NumEnable);
                }