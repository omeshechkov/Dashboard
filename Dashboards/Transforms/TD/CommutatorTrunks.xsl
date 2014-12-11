<xsl:stylesheet version="2.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
	<xsl:output method="xml" indent="yes"/>
	<xsl:template match="/">
		<InterconnectData>
			<OfficeDirections>
				<xsl:for-each select="/Schemas/Schema/CommutatorData/RawData/OfficeDirections/OfficeDirection[@showOnDashboard='True']">
					<OfficeDirection title="{@title}">
						<Idle>
							<xsl:value-of select="Counters/Idle[@counter='count']"/>
						</Idle>
						<Occupied>
							<xsl:value-of select="Counters/Occupied[@counter='count']"/>
						</Occupied>
						<Blocked>
							<xsl:value-of select="Counters/Blocked[@counter='count']"/>
						</Blocked>
						<Total>
							<xsl:value-of select="Counters/Total[@counter='count']"/>
						</Total>
						<CylinderData IsDisabled="False">
							<Counters>
								<Counter IsPrimary="True"
									NormalSeverity="0" LowSeverity="70" HighSeverity="85"
									Value="{Counters/Occupied[@counter='percent']}"
								>
									<Alarm>
										<xsl:attribute name="message" select="'Too many occupied channels. Count: {0}'"/>
										<Parameter>
											<xsl:value-of select="Counters/Occupied[@counter='percent']"/>
										</Parameter>
									</Alarm>
								</Counter>
							</Counters>
						</CylinderData>

						<xsl:copy-of select="GraphicData"/>

						<xsl:variable name="totalTraffic" select="sum(IncomingTraffic/Direction/@seconds) + sum(OutgoingTraffic/Direction/@seconds)"/>
            
						<IncomingTraffic>
							<xsl:for-each select="IncomingTraffic/Direction">
								<xsl:variable name="percent" select="round-half-to-even(@seconds div $totalTraffic * 100, 2)"/>
								<Direction name="{@name}" position="{@position}" totalMinutes="{@totalMinutes}" percent="{$percent}"/>
							</xsl:for-each>
						</IncomingTraffic>
						<OutgoingTraffic>
							<xsl:for-each select="OutgoingTraffic/Direction">
								<xsl:variable name="percent" select="round-half-to-even(@seconds div $totalTraffic * 100, 2)"/>
								<Direction name="{@name}" position="{@position}" totalMinutes="{@totalMinutes}" percent="{$percent}"/>
							</xsl:for-each>
						</OutgoingTraffic>

						<PercentSecondsIncoming>
							<Counters>
								<Counter IsPrimary="True" name="Incoming Traffic" description="Incoming traffic in percent"
									NormalSeverity="50" LowSeverity="30" HighSeverity="10"
									Value="{PercentSecondsIncoming}"
								>
									<Alarm>
										<xsl:attribute name="message" select="'Incoming traffic is {0}%'"/>
										<Parameter>
											<xsl:value-of select="PercentSecondsIncoming"/>
										</Parameter>
									</Alarm>
								</Counter>
							</Counters>
						</PercentSecondsIncoming>

          </OfficeDirection>
				</xsl:for-each>
			</OfficeDirections>
		</InterconnectData>
	</xsl:template>
</xsl:stylesheet><!-- Stylus Studio meta-information - (c) 2004-2008. Progress Software Corporation. All rights reserved.

<metaInformation>
	<scenarios>
		<scenario default="yes" name="Scenario1" userelativepaths="yes" externalpreview="no" url="file:///x:/Output/TD/CommutatorTrunks.xml" htmlbaseurl="" outputurl="" processortype="saxon8" useresolver="yes" profilemode="0" profiledepth="" profilelength=""
		          urlprofilexml="" commandline="" additionalpath="" additionalclasspath="" postprocessortype="none" postprocesscommandline="" postprocessadditionalpath="" postprocessgeneratedext="" validateoutput="no" validator="internal" customvalidator="">
			<advancedProp name="sInitialMode" value=""/>
			<advancedProp name="bXsltOneIsOkay" value="true"/>
			<advancedProp name="bSchemaAware" value="true"/>
			<advancedProp name="bXml11" value="false"/>
			<advancedProp name="iValidation" value="0"/>
			<advancedProp name="bExtensions" value="true"/>
			<advancedProp name="iWhitespace" value="0"/>
			<advancedProp name="sInitialTemplate" value=""/>
			<advancedProp name="bTinyTree" value="true"/>
			<advancedProp name="bWarnings" value="true"/>
			<advancedProp name="bUseDTD" value="false"/>
			<advancedProp name="iErrorHandling" value="fatal"/>
		</scenario>
	</scenarios>
	<MapperMetaTag>
		<MapperInfo srcSchemaPathIsRelative="yes" srcSchemaInterpretAsXML="no" destSchemaPath="" destSchemaRoot="" destSchemaPathIsRelative="yes" destSchemaInterpretAsXML="no">
			<SourceSchema srcSchemaPath="file:///x:/Output/TD/CommutatorTrunks.xml" srcSchemaRoot="Schemas" AssociatedInstance="" loaderFunction="document" loaderFunctionUsesURI="no"/>
		</MapperInfo>
		<MapperBlockPosition>
			<template match="/">
				<block path="InterconnectData/OfficeDirections/xsl:for-each" x="146" y="54"/>
				<block path="InterconnectData/OfficeDirections/xsl:for-each/OfficeDirection/IncomingTraffic/xsl:for-each" x="146" y="105"/>
				<block path="InterconnectData/OfficeDirections/xsl:for-each/OfficeDirection/OutgoingTraffic/xsl:for-each" x="186" y="105"/>
			</template>
		</MapperBlockPosition>
		<TemplateContext></TemplateContext>
		<MapperFilter side="source"></MapperFilter>
	</MapperMetaTag>
</metaInformation>
-->