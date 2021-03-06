﻿using Grpc.Net.Client;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using System.Text.Json;
using Tony.FileTransfer.Core.Common;

namespace Tony.FileTransfer.Server.Test.gRPC
{
    public class UserServiceTest
    {
        IUserService.IUserServiceClient client;
        public UserServiceTest()
        {
            var channel = GrpcChannel.ForAddress("https://localhost:5001", new GrpcChannelOptions() { MaxSendMessageSize = int.MaxValue, MaxReceiveMessageSize = int.MaxValue });
            client = new IUserService.IUserServiceClient(channel);
        }

        [Fact]
        public void RegisterTest()
        {
            var userName = "tony";
            var password = "123456";
            var result = client.Register(new CommonRequest() { Message = JsonSerializer.Serialize(new { UserName = userName, Password = password }) });
            Assert.True(true == result.Result);

        }
        [Fact]
        public void RepeatRegisterTest()
        {
            var userName = "tony";
            var password = "123456";

            var result = client.Register(new CommonRequest() { Message = JsonSerializer.Serialize(new { UserName = userName, Password = password }) });
            Assert.True(false == result.Result);
            Assert.True(result.ErrorCode == ErrorCodes.UserNameHasExist);

        }
        [Fact]
        public void NullUserRegisterTest()
        {
            var userName = "";
            var password = "123456";

            var result = client.Register(new CommonRequest() { Message = JsonSerializer.Serialize(new { UserName = userName, Password = password }) });
            Assert.True(false == result.Result);
            Assert.True(result.ErrorCode == ErrorCodes.UserNameIsNull);
        }
        [Fact]
        public void NullPasswordRegisterTest()
        {
            var userName = "tony123";
            var password = "";

            var result = client.Register(new CommonRequest() { Message = JsonSerializer.Serialize(new { UserName = userName, Password = password }) });
            Assert.True(false == result.Result);
            Assert.True(result.ErrorCode == ErrorCodes.InValidPassword);
        }

        [Fact]
        public void LoginTest()
        {
            var userName = "tony";
            var password = "123456";

            var result = client.Login(new CommonRequest() { Message = JsonSerializer.Serialize(new { UserName = userName, Password = password }) });
            Assert.True(true == result.Result.Result);
            Assert.True(!string.IsNullOrEmpty(result.Message));
        }
        [Fact]
        public void LoginTest_Error()
        {
            var userName = "fsfofeiw";
            var password = "fdso3wkrp";
            var result = client.Login(new CommonRequest() { Message = JsonSerializer.Serialize(new { UserName = userName, Password = password }) });
            Assert.True(false == result.Result.Result);
            Assert.True(string.IsNullOrEmpty(result.Message));
            Assert.True(result.Result.ErrorCode== ErrorCodes.InValidUserNameOrPassword);
        }
        //1b1d485767ed66a4
        //97b4f4ab7d6e4b1a 455286005 4880
        [Fact]
        public void GetRecognizedIdTest()
        {
            var identity = Guid.NewGuid().To16String();
            var result = client.GetRecognizedId(new CommonRequest() { Message = identity });
            Assert.True(result.Result.Result == true);
            var doc = JsonDocument.Parse(result.Message);
            Assert.True(doc.RootElement.GetProperty("RecognizeId").GetInt64().ToString().Length==9);
                 
        }

        [Fact]
        public void GetSameRecognizeIdTest()
        {
            var identity = "97b4f4ab7d6e4b1a";
            var result = client.GetRecognizedId(new CommonRequest() { Message = identity });
            Assert.True(result.Result.Result == true);
            var doc = JsonDocument.Parse(result.Message);
            Assert.True(doc.RootElement.GetProperty("RecognizeId").GetInt64().ToString()== "455286005");
            Assert.True(doc.RootElement.GetProperty("Password").GetString() == "4880");
        }
        
       

    }
}
