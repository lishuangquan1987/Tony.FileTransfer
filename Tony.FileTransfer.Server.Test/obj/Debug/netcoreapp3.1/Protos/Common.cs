// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: Protos/common.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace Tony.FileTransfer.Server {

  /// <summary>Holder for reflection information generated from Protos/common.proto</summary>
  public static partial class CommonReflection {

    #region Descriptor
    /// <summary>File descriptor for Protos/common.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static CommonReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "ChNQcm90b3MvY29tbW9uLnByb3RvEgZjb21tb24iRwoOQ29tbW9uUmVzcG9u",
            "c2USDgoGUmVzdWx0GAEgASgIEiUKCUVycm9yQ29kZRgCIAEoDjISLmNvbW1v",
            "bi5FcnJvckNvZGVzIlQKGUNvbW1vblJlc3BvbnNlV2l0aE1lc3NhZ2USJgoG",
            "UmVzdWx0GAEgASgLMhYuY29tbW9uLkNvbW1vblJlc3BvbnNlEg8KB01lc3Nh",
            "Z2UYAiABKAkiIAoNQ29tbW9uUmVxdWVzdBIPCgdtZXNzYWdlGAEgASgJKtID",
            "CgpFcnJvckNvZGVzEgsKB05vRXJyb3IQABIRCg1EYXRhQmFzZUVycm9yEAES",
            "IQodRmlsZUFscmVhZHlFeGlzdEluU2VydmVyQ2FjaGUQAhIeChpGaWxlQWxy",
            "ZWFkeUV4aXN0SW5TZXJ2ZXJEQhADEh0KGUZpbGVOb3RFeGlzdEluU2VydmVy",
            "Q2FjaGUQBBIaChZGaWxlTm90RXhpc3RJblNlcnZlckRCEAUSEgoOQmFkQ2Fs",
            "bENvbnRleHQQBhIXChNSZWNvZ25pemVJZE5vdEV4aXN0EAcSEAoMVXNlck5v",
            "dEV4aXN0EAgSFgoSVXNlck1hY2hpbmVOb3RCaW5kEAkSEwoPTUQ1Q29tcGFy",
            "ZUVycm9yEAoSFQoRQ2FjaGVGaWxlTm90RXhpc3QQCxIWChJJblZhbGlkUmVj",
            "b2duaXplSWQQDBIaChZJblZhbGlkTWFjaGluZUlkZW50aXR5EA0SEQoNSW5W",
            "YWxpZFVzZXJJZBAOEh0KGUluVmFsaWRVc2VyTmFtZU9yUGFzc3dvcmQQDxIS",
            "Cg5Vc2VyTmFtZUlzTnVsbBAQEhQKEFVzZXJOYW1lSGFzRXhpc3QQERITCg9J",
            "blZhbGlkUGFzc3dvcmQQEkIbqgIYVG9ueS5GaWxlVHJhbnNmZXIuU2VydmVy",
            "YgZwcm90bzM="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { },
          new pbr::GeneratedClrTypeInfo(new[] {typeof(global::Tony.FileTransfer.Server.ErrorCodes), }, null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::Tony.FileTransfer.Server.CommonResponse), global::Tony.FileTransfer.Server.CommonResponse.Parser, new[]{ "Result", "ErrorCode" }, null, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::Tony.FileTransfer.Server.CommonResponseWithMessage), global::Tony.FileTransfer.Server.CommonResponseWithMessage.Parser, new[]{ "Result", "Message" }, null, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::Tony.FileTransfer.Server.CommonRequest), global::Tony.FileTransfer.Server.CommonRequest.Parser, new[]{ "Message" }, null, null, null, null)
          }));
    }
    #endregion

  }
  #region Enums
  public enum ErrorCodes {
    [pbr::OriginalName("NoError")] NoError = 0,
    [pbr::OriginalName("DataBaseError")] DataBaseError = 1,
    [pbr::OriginalName("FileAlreadyExistInServerCache")] FileAlreadyExistInServerCache = 2,
    [pbr::OriginalName("FileAlreadyExistInServerDB")] FileAlreadyExistInServerDb = 3,
    [pbr::OriginalName("FileNotExistInServerCache")] FileNotExistInServerCache = 4,
    [pbr::OriginalName("FileNotExistInServerDB")] FileNotExistInServerDb = 5,
    [pbr::OriginalName("BadCallContext")] BadCallContext = 6,
    [pbr::OriginalName("RecognizeIdNotExist")] RecognizeIdNotExist = 7,
    [pbr::OriginalName("UserNotExist")] UserNotExist = 8,
    [pbr::OriginalName("UserMachineNotBind")] UserMachineNotBind = 9,
    [pbr::OriginalName("MD5CompareError")] Md5CompareError = 10,
    [pbr::OriginalName("CacheFileNotExist")] CacheFileNotExist = 11,
    [pbr::OriginalName("InValidRecognizeId")] InValidRecognizeId = 12,
    [pbr::OriginalName("InValidMachineIdentity")] InValidMachineIdentity = 13,
    [pbr::OriginalName("InValidUserId")] InValidUserId = 14,
    [pbr::OriginalName("InValidUserNameOrPassword")] InValidUserNameOrPassword = 15,
    [pbr::OriginalName("UserNameIsNull")] UserNameIsNull = 16,
    [pbr::OriginalName("UserNameHasExist")] UserNameHasExist = 17,
    [pbr::OriginalName("InValidPassword")] InValidPassword = 18,
  }

  #endregion

  #region Messages
  public sealed partial class CommonResponse : pb::IMessage<CommonResponse>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<CommonResponse> _parser = new pb::MessageParser<CommonResponse>(() => new CommonResponse());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<CommonResponse> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Tony.FileTransfer.Server.CommonReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public CommonResponse() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public CommonResponse(CommonResponse other) : this() {
      result_ = other.result_;
      errorCode_ = other.errorCode_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public CommonResponse Clone() {
      return new CommonResponse(this);
    }

    /// <summary>Field number for the "Result" field.</summary>
    public const int ResultFieldNumber = 1;
    private bool result_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Result {
      get { return result_; }
      set {
        result_ = value;
      }
    }

    /// <summary>Field number for the "ErrorCode" field.</summary>
    public const int ErrorCodeFieldNumber = 2;
    private global::Tony.FileTransfer.Server.ErrorCodes errorCode_ = global::Tony.FileTransfer.Server.ErrorCodes.NoError;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::Tony.FileTransfer.Server.ErrorCodes ErrorCode {
      get { return errorCode_; }
      set {
        errorCode_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as CommonResponse);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(CommonResponse other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (Result != other.Result) return false;
      if (ErrorCode != other.ErrorCode) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (Result != false) hash ^= Result.GetHashCode();
      if (ErrorCode != global::Tony.FileTransfer.Server.ErrorCodes.NoError) hash ^= ErrorCode.GetHashCode();
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      output.WriteRawMessage(this);
    #else
      if (Result != false) {
        output.WriteRawTag(8);
        output.WriteBool(Result);
      }
      if (ErrorCode != global::Tony.FileTransfer.Server.ErrorCodes.NoError) {
        output.WriteRawTag(16);
        output.WriteEnum((int) ErrorCode);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    void pb::IBufferMessage.InternalWriteTo(ref pb::WriteContext output) {
      if (Result != false) {
        output.WriteRawTag(8);
        output.WriteBool(Result);
      }
      if (ErrorCode != global::Tony.FileTransfer.Server.ErrorCodes.NoError) {
        output.WriteRawTag(16);
        output.WriteEnum((int) ErrorCode);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(ref output);
      }
    }
    #endif

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (Result != false) {
        size += 1 + 1;
      }
      if (ErrorCode != global::Tony.FileTransfer.Server.ErrorCodes.NoError) {
        size += 1 + pb::CodedOutputStream.ComputeEnumSize((int) ErrorCode);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(CommonResponse other) {
      if (other == null) {
        return;
      }
      if (other.Result != false) {
        Result = other.Result;
      }
      if (other.ErrorCode != global::Tony.FileTransfer.Server.ErrorCodes.NoError) {
        ErrorCode = other.ErrorCode;
      }
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      input.ReadRawMessage(this);
    #else
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 8: {
            Result = input.ReadBool();
            break;
          }
          case 16: {
            ErrorCode = (global::Tony.FileTransfer.Server.ErrorCodes) input.ReadEnum();
            break;
          }
        }
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    void pb::IBufferMessage.InternalMergeFrom(ref pb::ParseContext input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, ref input);
            break;
          case 8: {
            Result = input.ReadBool();
            break;
          }
          case 16: {
            ErrorCode = (global::Tony.FileTransfer.Server.ErrorCodes) input.ReadEnum();
            break;
          }
        }
      }
    }
    #endif

  }

  public sealed partial class CommonResponseWithMessage : pb::IMessage<CommonResponseWithMessage>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<CommonResponseWithMessage> _parser = new pb::MessageParser<CommonResponseWithMessage>(() => new CommonResponseWithMessage());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<CommonResponseWithMessage> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Tony.FileTransfer.Server.CommonReflection.Descriptor.MessageTypes[1]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public CommonResponseWithMessage() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public CommonResponseWithMessage(CommonResponseWithMessage other) : this() {
      result_ = other.result_ != null ? other.result_.Clone() : null;
      message_ = other.message_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public CommonResponseWithMessage Clone() {
      return new CommonResponseWithMessage(this);
    }

    /// <summary>Field number for the "Result" field.</summary>
    public const int ResultFieldNumber = 1;
    private global::Tony.FileTransfer.Server.CommonResponse result_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::Tony.FileTransfer.Server.CommonResponse Result {
      get { return result_; }
      set {
        result_ = value;
      }
    }

    /// <summary>Field number for the "Message" field.</summary>
    public const int MessageFieldNumber = 2;
    private string message_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string Message {
      get { return message_; }
      set {
        message_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as CommonResponseWithMessage);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(CommonResponseWithMessage other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (!object.Equals(Result, other.Result)) return false;
      if (Message != other.Message) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (result_ != null) hash ^= Result.GetHashCode();
      if (Message.Length != 0) hash ^= Message.GetHashCode();
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      output.WriteRawMessage(this);
    #else
      if (result_ != null) {
        output.WriteRawTag(10);
        output.WriteMessage(Result);
      }
      if (Message.Length != 0) {
        output.WriteRawTag(18);
        output.WriteString(Message);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    void pb::IBufferMessage.InternalWriteTo(ref pb::WriteContext output) {
      if (result_ != null) {
        output.WriteRawTag(10);
        output.WriteMessage(Result);
      }
      if (Message.Length != 0) {
        output.WriteRawTag(18);
        output.WriteString(Message);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(ref output);
      }
    }
    #endif

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (result_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(Result);
      }
      if (Message.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Message);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(CommonResponseWithMessage other) {
      if (other == null) {
        return;
      }
      if (other.result_ != null) {
        if (result_ == null) {
          Result = new global::Tony.FileTransfer.Server.CommonResponse();
        }
        Result.MergeFrom(other.Result);
      }
      if (other.Message.Length != 0) {
        Message = other.Message;
      }
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      input.ReadRawMessage(this);
    #else
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 10: {
            if (result_ == null) {
              Result = new global::Tony.FileTransfer.Server.CommonResponse();
            }
            input.ReadMessage(Result);
            break;
          }
          case 18: {
            Message = input.ReadString();
            break;
          }
        }
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    void pb::IBufferMessage.InternalMergeFrom(ref pb::ParseContext input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, ref input);
            break;
          case 10: {
            if (result_ == null) {
              Result = new global::Tony.FileTransfer.Server.CommonResponse();
            }
            input.ReadMessage(Result);
            break;
          }
          case 18: {
            Message = input.ReadString();
            break;
          }
        }
      }
    }
    #endif

  }

  public sealed partial class CommonRequest : pb::IMessage<CommonRequest>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<CommonRequest> _parser = new pb::MessageParser<CommonRequest>(() => new CommonRequest());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<CommonRequest> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Tony.FileTransfer.Server.CommonReflection.Descriptor.MessageTypes[2]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public CommonRequest() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public CommonRequest(CommonRequest other) : this() {
      message_ = other.message_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public CommonRequest Clone() {
      return new CommonRequest(this);
    }

    /// <summary>Field number for the "message" field.</summary>
    public const int MessageFieldNumber = 1;
    private string message_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string Message {
      get { return message_; }
      set {
        message_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as CommonRequest);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(CommonRequest other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (Message != other.Message) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (Message.Length != 0) hash ^= Message.GetHashCode();
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      output.WriteRawMessage(this);
    #else
      if (Message.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(Message);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    void pb::IBufferMessage.InternalWriteTo(ref pb::WriteContext output) {
      if (Message.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(Message);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(ref output);
      }
    }
    #endif

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (Message.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Message);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(CommonRequest other) {
      if (other == null) {
        return;
      }
      if (other.Message.Length != 0) {
        Message = other.Message;
      }
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      input.ReadRawMessage(this);
    #else
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 10: {
            Message = input.ReadString();
            break;
          }
        }
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    void pb::IBufferMessage.InternalMergeFrom(ref pb::ParseContext input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, ref input);
            break;
          case 10: {
            Message = input.ReadString();
            break;
          }
        }
      }
    }
    #endif

  }

  #endregion

}

#endregion Designer generated code
