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
            "ChNQcm90b3MvY29tbW9uLnByb3RvEgZjb21tb24iMwoOQ29tbW9uUmVzcG9u",
            "c2USDgoGUmVzdWx0GAEgASgIEhEKCUVycm9yQ29kZRgCIAEoBUIbqgIYVG9u",
            "eS5GaWxlVHJhbnNmZXIuU2VydmVyYgZwcm90bzM="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { },
          new pbr::GeneratedClrTypeInfo(null, null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::Tony.FileTransfer.Server.CommonResponse), global::Tony.FileTransfer.Server.CommonResponse.Parser, new[]{ "Result", "ErrorCode" }, null, null, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  public sealed partial class CommonResponse : pb::IMessage<CommonResponse> {
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
    private int errorCode_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int ErrorCode {
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
      if (ErrorCode != 0) hash ^= ErrorCode.GetHashCode();
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
      if (Result != false) {
        output.WriteRawTag(8);
        output.WriteBool(Result);
      }
      if (ErrorCode != 0) {
        output.WriteRawTag(16);
        output.WriteInt32(ErrorCode);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (Result != false) {
        size += 1 + 1;
      }
      if (ErrorCode != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(ErrorCode);
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
      if (other.ErrorCode != 0) {
        ErrorCode = other.ErrorCode;
      }
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
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
            ErrorCode = input.ReadInt32();
            break;
          }
        }
      }
    }

  }

  #endregion

}

#endregion Designer generated code
