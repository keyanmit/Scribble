<?xml version="1.0" encoding="utf-8"?>
<serviceModel xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" name="Scribble" generation="1" functional="0" release="0" Id="870f338a-c5b6-4302-8936-2f1d76ebc654" dslVersion="1.2.0.0" xmlns="http://schemas.microsoft.com/dsltools/RDSM">
  <groups>
    <group name="ScribbleGroup" generation="1" functional="0" release="0">
      <componentports>
        <inPort name="RenderingWebEndpoint:Endpoint1" protocol="http">
          <inToChannel>
            <lBChannelMoniker name="/Scribble/ScribbleGroup/LB:RenderingWebEndpoint:Endpoint1" />
          </inToChannel>
        </inPort>
        <inPort name="UploadWebAPIEndpoint:Endpoint1" protocol="http">
          <inToChannel>
            <lBChannelMoniker name="/Scribble/ScribbleGroup/LB:UploadWebAPIEndpoint:Endpoint1" />
          </inToChannel>
        </inPort>
      </componentports>
      <settings>
        <aCS name="EncryptionWorkerRole:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/Scribble/ScribbleGroup/MapEncryptionWorkerRole:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </maps>
        </aCS>
        <aCS name="EncryptionWorkerRole:Scribble.ConnectionStrings.CommonStorage.DevBox" defaultValue="">
          <maps>
            <mapMoniker name="/Scribble/ScribbleGroup/MapEncryptionWorkerRole:Scribble.ConnectionStrings.CommonStorage.DevBox" />
          </maps>
        </aCS>
        <aCS name="EncryptionWorkerRole:Scribble.QueueNames.TaskListQueue" defaultValue="">
          <maps>
            <mapMoniker name="/Scribble/ScribbleGroup/MapEncryptionWorkerRole:Scribble.QueueNames.TaskListQueue" />
          </maps>
        </aCS>
        <aCS name="EncryptionWorkerRole:Scribble.TableNames.TaskListPersistTable" defaultValue="">
          <maps>
            <mapMoniker name="/Scribble/ScribbleGroup/MapEncryptionWorkerRole:Scribble.TableNames.TaskListPersistTable" />
          </maps>
        </aCS>
        <aCS name="EncryptionWorkerRoleInstances" defaultValue="[1,1,1]">
          <maps>
            <mapMoniker name="/Scribble/ScribbleGroup/MapEncryptionWorkerRoleInstances" />
          </maps>
        </aCS>
        <aCS name="RenderingWebEndpoint:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/Scribble/ScribbleGroup/MapRenderingWebEndpoint:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </maps>
        </aCS>
        <aCS name="RenderingWebEndpoint:Scribble.ConnectionStrings.CommonStorage.DevBox" defaultValue="">
          <maps>
            <mapMoniker name="/Scribble/ScribbleGroup/MapRenderingWebEndpoint:Scribble.ConnectionStrings.CommonStorage.DevBox" />
          </maps>
        </aCS>
        <aCS name="RenderingWebEndpoint:Scribble.QueueNames.TaskListQueue" defaultValue="">
          <maps>
            <mapMoniker name="/Scribble/ScribbleGroup/MapRenderingWebEndpoint:Scribble.QueueNames.TaskListQueue" />
          </maps>
        </aCS>
        <aCS name="RenderingWebEndpoint:Scribble.TableNames.TaskListPersistTable" defaultValue="">
          <maps>
            <mapMoniker name="/Scribble/ScribbleGroup/MapRenderingWebEndpoint:Scribble.TableNames.TaskListPersistTable" />
          </maps>
        </aCS>
        <aCS name="RenderingWebEndpointInstances" defaultValue="[1,1,1]">
          <maps>
            <mapMoniker name="/Scribble/ScribbleGroup/MapRenderingWebEndpointInstances" />
          </maps>
        </aCS>
        <aCS name="UploadWebAPIEndpoint:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/Scribble/ScribbleGroup/MapUploadWebAPIEndpoint:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </maps>
        </aCS>
        <aCS name="UploadWebAPIEndpoint:Scribble.ConnectionStrings.CommonStorage.DevBox" defaultValue="">
          <maps>
            <mapMoniker name="/Scribble/ScribbleGroup/MapUploadWebAPIEndpoint:Scribble.ConnectionStrings.CommonStorage.DevBox" />
          </maps>
        </aCS>
        <aCS name="UploadWebAPIEndpoint:Scribble.QueueNames.TaskListQueue" defaultValue="">
          <maps>
            <mapMoniker name="/Scribble/ScribbleGroup/MapUploadWebAPIEndpoint:Scribble.QueueNames.TaskListQueue" />
          </maps>
        </aCS>
        <aCS name="UploadWebAPIEndpoint:Scribble.TableNames.TaskListPersistTable" defaultValue="">
          <maps>
            <mapMoniker name="/Scribble/ScribbleGroup/MapUploadWebAPIEndpoint:Scribble.TableNames.TaskListPersistTable" />
          </maps>
        </aCS>
        <aCS name="UploadWebAPIEndpointInstances" defaultValue="[1,1,1]">
          <maps>
            <mapMoniker name="/Scribble/ScribbleGroup/MapUploadWebAPIEndpointInstances" />
          </maps>
        </aCS>
      </settings>
      <channels>
        <lBChannel name="LB:RenderingWebEndpoint:Endpoint1">
          <toPorts>
            <inPortMoniker name="/Scribble/ScribbleGroup/RenderingWebEndpoint/Endpoint1" />
          </toPorts>
        </lBChannel>
        <lBChannel name="LB:UploadWebAPIEndpoint:Endpoint1">
          <toPorts>
            <inPortMoniker name="/Scribble/ScribbleGroup/UploadWebAPIEndpoint/Endpoint1" />
          </toPorts>
        </lBChannel>
      </channels>
      <maps>
        <map name="MapEncryptionWorkerRole:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/Scribble/ScribbleGroup/EncryptionWorkerRole/Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </setting>
        </map>
        <map name="MapEncryptionWorkerRole:Scribble.ConnectionStrings.CommonStorage.DevBox" kind="Identity">
          <setting>
            <aCSMoniker name="/Scribble/ScribbleGroup/EncryptionWorkerRole/Scribble.ConnectionStrings.CommonStorage.DevBox" />
          </setting>
        </map>
        <map name="MapEncryptionWorkerRole:Scribble.QueueNames.TaskListQueue" kind="Identity">
          <setting>
            <aCSMoniker name="/Scribble/ScribbleGroup/EncryptionWorkerRole/Scribble.QueueNames.TaskListQueue" />
          </setting>
        </map>
        <map name="MapEncryptionWorkerRole:Scribble.TableNames.TaskListPersistTable" kind="Identity">
          <setting>
            <aCSMoniker name="/Scribble/ScribbleGroup/EncryptionWorkerRole/Scribble.TableNames.TaskListPersistTable" />
          </setting>
        </map>
        <map name="MapEncryptionWorkerRoleInstances" kind="Identity">
          <setting>
            <sCSPolicyIDMoniker name="/Scribble/ScribbleGroup/EncryptionWorkerRoleInstances" />
          </setting>
        </map>
        <map name="MapRenderingWebEndpoint:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/Scribble/ScribbleGroup/RenderingWebEndpoint/Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </setting>
        </map>
        <map name="MapRenderingWebEndpoint:Scribble.ConnectionStrings.CommonStorage.DevBox" kind="Identity">
          <setting>
            <aCSMoniker name="/Scribble/ScribbleGroup/RenderingWebEndpoint/Scribble.ConnectionStrings.CommonStorage.DevBox" />
          </setting>
        </map>
        <map name="MapRenderingWebEndpoint:Scribble.QueueNames.TaskListQueue" kind="Identity">
          <setting>
            <aCSMoniker name="/Scribble/ScribbleGroup/RenderingWebEndpoint/Scribble.QueueNames.TaskListQueue" />
          </setting>
        </map>
        <map name="MapRenderingWebEndpoint:Scribble.TableNames.TaskListPersistTable" kind="Identity">
          <setting>
            <aCSMoniker name="/Scribble/ScribbleGroup/RenderingWebEndpoint/Scribble.TableNames.TaskListPersistTable" />
          </setting>
        </map>
        <map name="MapRenderingWebEndpointInstances" kind="Identity">
          <setting>
            <sCSPolicyIDMoniker name="/Scribble/ScribbleGroup/RenderingWebEndpointInstances" />
          </setting>
        </map>
        <map name="MapUploadWebAPIEndpoint:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/Scribble/ScribbleGroup/UploadWebAPIEndpoint/Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </setting>
        </map>
        <map name="MapUploadWebAPIEndpoint:Scribble.ConnectionStrings.CommonStorage.DevBox" kind="Identity">
          <setting>
            <aCSMoniker name="/Scribble/ScribbleGroup/UploadWebAPIEndpoint/Scribble.ConnectionStrings.CommonStorage.DevBox" />
          </setting>
        </map>
        <map name="MapUploadWebAPIEndpoint:Scribble.QueueNames.TaskListQueue" kind="Identity">
          <setting>
            <aCSMoniker name="/Scribble/ScribbleGroup/UploadWebAPIEndpoint/Scribble.QueueNames.TaskListQueue" />
          </setting>
        </map>
        <map name="MapUploadWebAPIEndpoint:Scribble.TableNames.TaskListPersistTable" kind="Identity">
          <setting>
            <aCSMoniker name="/Scribble/ScribbleGroup/UploadWebAPIEndpoint/Scribble.TableNames.TaskListPersistTable" />
          </setting>
        </map>
        <map name="MapUploadWebAPIEndpointInstances" kind="Identity">
          <setting>
            <sCSPolicyIDMoniker name="/Scribble/ScribbleGroup/UploadWebAPIEndpointInstances" />
          </setting>
        </map>
      </maps>
      <components>
        <groupHascomponents>
          <role name="EncryptionWorkerRole" generation="1" functional="0" release="0" software="D:\thakali\Scribble\Scribble\Scribble\csx\Debug\roles\EncryptionWorkerRole" entryPoint="base\x64\WaHostBootstrapper.exe" parameters="base\x64\WaWorkerHost.exe " memIndex="-1" hostingEnvironment="consoleroleadmin" hostingEnvironmentVersion="2">
            <settings>
              <aCS name="Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="" />
              <aCS name="Scribble.ConnectionStrings.CommonStorage.DevBox" defaultValue="" />
              <aCS name="Scribble.QueueNames.TaskListQueue" defaultValue="" />
              <aCS name="Scribble.TableNames.TaskListPersistTable" defaultValue="" />
              <aCS name="__ModelData" defaultValue="&lt;m role=&quot;EncryptionWorkerRole&quot; xmlns=&quot;urn:azure:m:v1&quot;&gt;&lt;r name=&quot;EncryptionWorkerRole&quot; /&gt;&lt;r name=&quot;RenderingWebEndpoint&quot;&gt;&lt;e name=&quot;Endpoint1&quot; /&gt;&lt;/r&gt;&lt;r name=&quot;UploadWebAPIEndpoint&quot;&gt;&lt;e name=&quot;Endpoint1&quot; /&gt;&lt;/r&gt;&lt;/m&gt;" />
            </settings>
            <resourcereferences>
              <resourceReference name="DiagnosticStore" defaultAmount="[4096,4096,4096]" defaultSticky="true" kind="Directory" />
              <resourceReference name="EventStore" defaultAmount="[1000,1000,1000]" defaultSticky="false" kind="LogStore" />
            </resourcereferences>
          </role>
          <sCSPolicy>
            <sCSPolicyIDMoniker name="/Scribble/ScribbleGroup/EncryptionWorkerRoleInstances" />
            <sCSPolicyUpdateDomainMoniker name="/Scribble/ScribbleGroup/EncryptionWorkerRoleUpgradeDomains" />
            <sCSPolicyFaultDomainMoniker name="/Scribble/ScribbleGroup/EncryptionWorkerRoleFaultDomains" />
          </sCSPolicy>
        </groupHascomponents>
        <groupHascomponents>
          <role name="RenderingWebEndpoint" generation="1" functional="0" release="0" software="D:\thakali\Scribble\Scribble\Scribble\csx\Debug\roles\RenderingWebEndpoint" entryPoint="base\x64\WaHostBootstrapper.exe" parameters="base\x64\WaIISHost.exe " memIndex="-1" hostingEnvironment="frontendadmin" hostingEnvironmentVersion="2">
            <componentports>
              <inPort name="Endpoint1" protocol="http" portRanges="80" />
            </componentports>
            <settings>
              <aCS name="Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="" />
              <aCS name="Scribble.ConnectionStrings.CommonStorage.DevBox" defaultValue="" />
              <aCS name="Scribble.QueueNames.TaskListQueue" defaultValue="" />
              <aCS name="Scribble.TableNames.TaskListPersistTable" defaultValue="" />
              <aCS name="__ModelData" defaultValue="&lt;m role=&quot;RenderingWebEndpoint&quot; xmlns=&quot;urn:azure:m:v1&quot;&gt;&lt;r name=&quot;EncryptionWorkerRole&quot; /&gt;&lt;r name=&quot;RenderingWebEndpoint&quot;&gt;&lt;e name=&quot;Endpoint1&quot; /&gt;&lt;/r&gt;&lt;r name=&quot;UploadWebAPIEndpoint&quot;&gt;&lt;e name=&quot;Endpoint1&quot; /&gt;&lt;/r&gt;&lt;/m&gt;" />
            </settings>
            <resourcereferences>
              <resourceReference name="DiagnosticStore" defaultAmount="[4096,4096,4096]" defaultSticky="true" kind="Directory" />
              <resourceReference name="EventStore" defaultAmount="[1000,1000,1000]" defaultSticky="false" kind="LogStore" />
            </resourcereferences>
          </role>
          <sCSPolicy>
            <sCSPolicyIDMoniker name="/Scribble/ScribbleGroup/RenderingWebEndpointInstances" />
            <sCSPolicyUpdateDomainMoniker name="/Scribble/ScribbleGroup/RenderingWebEndpointUpgradeDomains" />
            <sCSPolicyFaultDomainMoniker name="/Scribble/ScribbleGroup/RenderingWebEndpointFaultDomains" />
          </sCSPolicy>
        </groupHascomponents>
        <groupHascomponents>
          <role name="UploadWebAPIEndpoint" generation="1" functional="0" release="0" software="D:\thakali\Scribble\Scribble\Scribble\csx\Debug\roles\UploadWebAPIEndpoint" entryPoint="base\x64\WaHostBootstrapper.exe" parameters="base\x64\WaIISHost.exe " memIndex="-1" hostingEnvironment="frontendadmin" hostingEnvironmentVersion="2">
            <componentports>
              <inPort name="Endpoint1" protocol="http" portRanges="8080" />
            </componentports>
            <settings>
              <aCS name="Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="" />
              <aCS name="Scribble.ConnectionStrings.CommonStorage.DevBox" defaultValue="" />
              <aCS name="Scribble.QueueNames.TaskListQueue" defaultValue="" />
              <aCS name="Scribble.TableNames.TaskListPersistTable" defaultValue="" />
              <aCS name="__ModelData" defaultValue="&lt;m role=&quot;UploadWebAPIEndpoint&quot; xmlns=&quot;urn:azure:m:v1&quot;&gt;&lt;r name=&quot;EncryptionWorkerRole&quot; /&gt;&lt;r name=&quot;RenderingWebEndpoint&quot;&gt;&lt;e name=&quot;Endpoint1&quot; /&gt;&lt;/r&gt;&lt;r name=&quot;UploadWebAPIEndpoint&quot;&gt;&lt;e name=&quot;Endpoint1&quot; /&gt;&lt;/r&gt;&lt;/m&gt;" />
            </settings>
            <resourcereferences>
              <resourceReference name="DiagnosticStore" defaultAmount="[4096,4096,4096]" defaultSticky="true" kind="Directory" />
              <resourceReference name="EventStore" defaultAmount="[1000,1000,1000]" defaultSticky="false" kind="LogStore" />
            </resourcereferences>
          </role>
          <sCSPolicy>
            <sCSPolicyIDMoniker name="/Scribble/ScribbleGroup/UploadWebAPIEndpointInstances" />
            <sCSPolicyUpdateDomainMoniker name="/Scribble/ScribbleGroup/UploadWebAPIEndpointUpgradeDomains" />
            <sCSPolicyFaultDomainMoniker name="/Scribble/ScribbleGroup/UploadWebAPIEndpointFaultDomains" />
          </sCSPolicy>
        </groupHascomponents>
      </components>
      <sCSPolicy>
        <sCSPolicyUpdateDomain name="RenderingWebEndpointUpgradeDomains" defaultPolicy="[5,5,5]" />
        <sCSPolicyUpdateDomain name="UploadWebAPIEndpointUpgradeDomains" defaultPolicy="[5,5,5]" />
        <sCSPolicyUpdateDomain name="EncryptionWorkerRoleUpgradeDomains" defaultPolicy="[5,5,5]" />
        <sCSPolicyFaultDomain name="EncryptionWorkerRoleFaultDomains" defaultPolicy="[2,2,2]" />
        <sCSPolicyFaultDomain name="RenderingWebEndpointFaultDomains" defaultPolicy="[2,2,2]" />
        <sCSPolicyFaultDomain name="UploadWebAPIEndpointFaultDomains" defaultPolicy="[2,2,2]" />
        <sCSPolicyID name="EncryptionWorkerRoleInstances" defaultPolicy="[1,1,1]" />
        <sCSPolicyID name="RenderingWebEndpointInstances" defaultPolicy="[1,1,1]" />
        <sCSPolicyID name="UploadWebAPIEndpointInstances" defaultPolicy="[1,1,1]" />
      </sCSPolicy>
    </group>
  </groups>
  <implements>
    <implementation Id="33a48333-6b1a-41b1-8377-37f88c2cca6c" ref="Microsoft.RedDog.Contract\ServiceContract\ScribbleContract@ServiceDefinition">
      <interfacereferences>
        <interfaceReference Id="956f5447-2d6e-4891-b8c4-04c7d8dc0428" ref="Microsoft.RedDog.Contract\Interface\RenderingWebEndpoint:Endpoint1@ServiceDefinition">
          <inPort>
            <inPortMoniker name="/Scribble/ScribbleGroup/RenderingWebEndpoint:Endpoint1" />
          </inPort>
        </interfaceReference>
        <interfaceReference Id="6c34ad50-76ba-4a15-be47-7c141a0d27d0" ref="Microsoft.RedDog.Contract\Interface\UploadWebAPIEndpoint:Endpoint1@ServiceDefinition">
          <inPort>
            <inPortMoniker name="/Scribble/ScribbleGroup/UploadWebAPIEndpoint:Endpoint1" />
          </inPort>
        </interfaceReference>
      </interfacereferences>
    </implementation>
  </implements>
</serviceModel>