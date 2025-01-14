﻿using Communicate.Logics;

namespace GameServer.Networks.Packets.Request
{
    public class RequestPing : ARecvPacket
    {
        public override void ExecuteRead()
        {
            ReadD(); // unk
        }

        public override void Process()
        {
            GlobalLogic
                .ClientPing(session);
        }
    }
}
