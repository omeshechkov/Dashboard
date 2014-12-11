<xsl:stylesheet version="2.0"
                xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
                xmlns:xs="http://www.w3.org/2001/XMLSchema"
                xmlns:saxon="http://saxon.sf.net/"
                xmlns:local="http://local.enterprise"
                exclude-result-prefixes="saxon xs local">

  <xsl:template match='/'>
    <TransformedSchemas>
      <UDAGs>
        <UDAG_172_27_141_21>
          <xsl:for-each select="Schemas/Schema[@name = '172.27.141.21 UDAG Data']/UDAGsSnapshot/RawData/UDAGs/UDAG" >
            <xsl:variable name="id" select="@id" />
            
            <xsl:for-each select="Connections/Connection" >
              <LinkRequestPerTime IsDisabled="False" idUDAG="{$id}" name="{@name}">
                <Counters>
                  <Counter name="One"  NormalSeverity="0" LowSeverity="120" HighSeverity="150" Value="{@requestPerTime}" IsPrimary="True" >
                    <Alarm>
                      <xsl:attribute name="message" select="'Request Per Time is {0}'" />

                      <Parameter>
                        <xsl:value-of select="@requestPerTime" />
                      </Parameter>
                    </Alarm>
                  </Counter>
                </Counters>
              </LinkRequestPerTime>
              
              <LinkReplyPerTime IsDisabled="False" idUDAG="{$id}" name="{@name}">
                <Counters>
                  <Counter name="One"  NormalSeverity="0" LowSeverity="120" HighSeverity="150" Value="{@replyPerTime}" IsPrimary="True" >
                    <Alarm>
                      <xsl:attribute name="message" select="'Reply Per Time is {0}'" />

                      <Parameter>
                        <xsl:value-of select="@replyPerTime" />
                      </Parameter>
                    </Alarm>
                  </Counter>
                </Counters>
              </LinkReplyPerTime>
              <ProgressBarQueue IsDisabled="False" idUDAG="{$id}" name="{@name}">
                <Counters>
                  <Counter name="One"  NormalSeverity="0" LowSeverity="100" HighSeverity="500" Value="{@inQueue}" IsPrimary="True" >
                    <Alarm>
                      <xsl:attribute name="message" select="'Queue is {0}'" />

                      <Parameter>
                        <xsl:value-of select="@inQueue" />
                      </Parameter>
                    </Alarm>
                  </Counter>
                </Counters>
              </ProgressBarQueue>
            </xsl:for-each>
          </xsl:for-each>
          
          <xsl:for-each select="Schemas/Schema[@name = '172.27.141.21 UDAG Data']/UDAGsSnapshot/RawData/GraphicData/Graphic" >
            <LinkGraphicData IsDisabled="False" name="{@name}" description="{@description}">
              <xsl:copy-of select="Values/Value"/>
            </LinkGraphicData>
          </xsl:for-each>
        </UDAG_172_27_141_21>
        
        <UDAG_172_27_141_22>
          <xsl:for-each select="Schemas/Schema[@name = '172.27.141.22 UDAG Data']/UDAGsSnapshot/RawData/UDAGs/UDAG" >
            <xsl:variable name="id" select="@id" />
            
            <xsl:for-each select="Connections/Connection" >
              <LinkRequestPerTime IsDisabled="False" idUDAG="{$id}" name="{@name}">
                <Counters>
                  <Counter name="One"  NormalSeverity="0" LowSeverity="120" HighSeverity="150" Value="{@requestPerTime}" IsPrimary="True" >
                    <Alarm>
                      <xsl:attribute name="message" select="'Request Per Time is {0}'" />

                      <Parameter>
                        <xsl:value-of select="@requestPerTime" />
                      </Parameter>
                    </Alarm>
                  </Counter>
                </Counters>
              </LinkRequestPerTime>
              
              <LinkReplyPerTime IsDisabled="False" idUDAG="{$id}" name="{@name}">
                <Counters>
                  <Counter name="One"  NormalSeverity="0" LowSeverity="120" HighSeverity="150" Value="{@replyPerTime}" IsPrimary="True" >
                    <Alarm>
                      <xsl:attribute name="message" select="'Reply Per Time is {0}'" />

                      <Parameter>
                        <xsl:value-of select="@replyPerTime" />
                      </Parameter>
                    </Alarm>
                  </Counter>
                </Counters>
              </LinkReplyPerTime>
              
              <ProgressBarQueue IsDisabled="False" idUDAG="{$id}" name="{@name}">
                <Counters>
                  <Counter name="One"  NormalSeverity="0" LowSeverity="100" HighSeverity="500" Value="{@inQueue}" IsPrimary="True" >
                    <Alarm>
                      <xsl:attribute name="message" select="'Queue is {0}'" />

                      <Parameter>
                        <xsl:value-of select="@inQueue" />
                      </Parameter>
                    </Alarm>
                  </Counter>
                </Counters>
              </ProgressBarQueue>
            </xsl:for-each>
          </xsl:for-each>
          
          <xsl:for-each select="Schemas/Schema[@name = '172.27.141.22 UDAG Data']/UDAGsSnapshot/RawData/GraphicData/Graphic" >
            <LinkGraphicData IsDisabled="False" name="{@name}" description="{@description}">
              <xsl:copy-of select="Values/Value"/>
            </LinkGraphicData>
          </xsl:for-each>
        </UDAG_172_27_141_22>
      </UDAGs>
      
      <TimesTenBalance IsDisabled="False">
        <xsl:variable name="averageTransactionsSentTTRating1" select="Schemas/Schema[@name = 'tt_rating1 TimesTen Instances Statistic Schema']/ReplicationPeersSchema/RawData/ReplicationPeer/DestinationInstances/DestinationInstance/TransactionsSent/Average" />
        <xsl:variable name="averageTransactionsReceivedTTRating1" select="Schemas/Schema[@name = 'tt_rating1 TimesTen Instances Statistic Schema']/ReplicationPeersSchema/RawData/ReplicationPeer/DestinationInstances/DestinationInstance/TransactionsReceived/Average" />
        <xsl:variable name="averageTransactionsSentTTRating2" select="Schemas/Schema[@name = 'tt_rating2 TimesTen Instances Statistic Schema']/ReplicationPeersSchema/RawData/ReplicationPeer/DestinationInstances/DestinationInstance/TransactionsSent/Average" />
        <xsl:variable name="averageTransactionsReceivedTTRating2" select="Schemas/Schema[@name = 'tt_rating2 TimesTen Instances Statistic Schema']/ReplicationPeersSchema/RawData/ReplicationPeer/DestinationInstances/DestinationInstance/TransactionsReceived/Average" />

        <xsl:variable name="sumAverageTransactionsTTRating1" select="$averageTransactionsSentTTRating1 + $averageTransactionsReceivedTTRating2" />
        <xsl:variable name="sumAverageTransactionsTTRating2" select="$averageTransactionsSentTTRating2 + $averageTransactionsReceivedTTRating1" />
        <xsl:variable name="sumAverageTransactionsTT" select="$sumAverageTransactionsTTRating1 + $sumAverageTransactionsTTRating2" />

        <xsl:variable name="percentBalanseTTRating1" select="(100 div $sumAverageTransactionsTT) * $sumAverageTransactionsTTRating1" />
        <xsl:variable name="percentBalanseTTRating2" select="(100 div $sumAverageTransactionsTT) * $sumAverageTransactionsTTRating2" />
        <xsl:variable name="percentBalanseTT" select="round-half-to-even(100 - abs($percentBalanseTTRating1 - $percentBalanseTTRating2),2)" />
        <Counters>
          <Counter name="One"  NormalSeverity="83" LowSeverity="68" HighSeverity="0" Value="{$percentBalanseTT}" IsPrimary="True" >
            <Alarm>
              <xsl:attribute name="message" select="'% Transactions Sent is {0}'" />

              <Parameter>
                <xsl:value-of select="$percentBalanseTT" />
              </Parameter>
            </Alarm>
          </Counter>
        </Counters>
      </TimesTenBalance>
      
      <TimesTenTransactionsRating1 IsDisabled="False">
        <xsl:variable name="averageTransactionsSentTTRating1" select="Schemas/Schema[@name = 'tt_rating1 TimesTen Instances Statistic Schema']/ReplicationPeersSchema/RawData/ReplicationPeer/DestinationInstances/DestinationInstance/TransactionsSent/Average" />
        <xsl:variable name="averageTransactionsReceivedTTRating1" select="Schemas/Schema[@name = 'tt_rating1 TimesTen Instances Statistic Schema']/ReplicationPeersSchema/RawData/ReplicationPeer/DestinationInstances/DestinationInstance/TransactionsReceived/Average" />
        <xsl:variable name="averageTransactionsSentTTRating2" select="Schemas/Schema[@name = 'tt_rating2 TimesTen Instances Statistic Schema']/ReplicationPeersSchema/RawData/ReplicationPeer/DestinationInstances/DestinationInstance/TransactionsSent/Average" />
        <xsl:variable name="averageTransactionsReceivedTTRating2" select="Schemas/Schema[@name = 'tt_rating2 TimesTen Instances Statistic Schema']/ReplicationPeersSchema/RawData/ReplicationPeer/DestinationInstances/DestinationInstance/TransactionsReceived/Average" />

        <xsl:variable name="sumAverageTransactionsTTRating1" select="round-half-to-even($averageTransactionsSentTTRating1 + $averageTransactionsReceivedTTRating2, 2)" />
        <xsl:variable name="sumAverageTransactionsTTRating2" select="round-half-to-even($averageTransactionsSentTTRating2 + $averageTransactionsReceivedTTRating1, 2)" />
        <xsl:variable name="sumAverageTransactionsTT" select="round-half-to-even($sumAverageTransactionsTTRating1 + $sumAverageTransactionsTTRating2, 2)" />

        <xsl:variable name="percentBalanseTTRating1" select="round-half-to-even((100 div $sumAverageTransactionsTT) * $sumAverageTransactionsTTRating1, 2)" />
        <xsl:variable name="percentBalanseTTRating2" select="round-half-to-even((100 div $sumAverageTransactionsTT) * $sumAverageTransactionsTTRating2)" />
        <xsl:variable name="percentBalanseTT" select="round-half-to-even(100 - abs($percentBalanseTTRating1 - $percentBalanseTTRating2), 2)" />
        
        <Counters>
          <Counter name="One"  NormalSeverity="10000000" LowSeverity="20000000" HighSeverity="30000000" Value="{$sumAverageTransactionsTTRating1}" IsPrimary="True" >
            <Alarm>
              <xsl:attribute name="message" select="'Rating1 Count Average Transactions Sent is {0}'" />

              <Parameter>
                <xsl:value-of select="$sumAverageTransactionsTTRating1" />
              </Parameter>
            </Alarm>
          </Counter>
          
          <Counter name="Two"  NormalSeverity="83" LowSeverity="68" HighSeverity="0" Value="{$percentBalanseTT}" >
            <Alarm>
              <xsl:attribute name="message" select="'% Transactions Sent is {0}'" />

              <Parameter>
                <xsl:value-of select="$percentBalanseTT" />
              </Parameter>
            </Alarm>
          </Counter>
        </Counters>
      </TimesTenTransactionsRating1>
      
      <TimesTenTransactionsRating2 IsDisabled="False">
        <xsl:variable name="averageTransactionsSentTTRating1" select="Schemas/Schema[@name = 'tt_rating1 TimesTen Instances Statistic Schema']/ReplicationPeersSchema/RawData/ReplicationPeer/DestinationInstances/DestinationInstance/TransactionsSent/Average" />
        <xsl:variable name="averageTransactionsReceivedTTRating1" select="Schemas/Schema[@name = 'tt_rating1 TimesTen Instances Statistic Schema']/ReplicationPeersSchema/RawData/ReplicationPeer/DestinationInstances/DestinationInstance/TransactionsReceived/Average" />
        <xsl:variable name="averageTransactionsSentTTRating2" select="Schemas/Schema[@name = 'tt_rating2 TimesTen Instances Statistic Schema']/ReplicationPeersSchema/RawData/ReplicationPeer/DestinationInstances/DestinationInstance/TransactionsSent/Average" />
        <xsl:variable name="averageTransactionsReceivedTTRating2" select="Schemas/Schema[@name = 'tt_rating2 TimesTen Instances Statistic Schema']/ReplicationPeersSchema/RawData/ReplicationPeer/DestinationInstances/DestinationInstance/TransactionsReceived/Average" />

        <xsl:variable name="sumAverageTransactionsTTRating1" select="round-half-to-even($averageTransactionsSentTTRating1 + $averageTransactionsReceivedTTRating2, 2)" />
        <xsl:variable name="sumAverageTransactionsTTRating2" select="round-half-to-even($averageTransactionsSentTTRating2 + $averageTransactionsReceivedTTRating1, 2)" />
        <xsl:variable name="sumAverageTransactionsTT" select="round-half-to-even($sumAverageTransactionsTTRating1 + $sumAverageTransactionsTTRating2, 2)" />

        <xsl:variable name="percentBalanseTTRating1" select="round-half-to-even((100 div $sumAverageTransactionsTT) * $sumAverageTransactionsTTRating1, 2)" />
        <xsl:variable name="percentBalanseTTRating2" select="round-half-to-even((100 div $sumAverageTransactionsTT) * $sumAverageTransactionsTTRating2, 2)" />
        <xsl:variable name="percentBalanseTT" select="round-half-to-even(100 - abs($percentBalanseTTRating1 - $percentBalanseTTRating2), 2)" />

        <Counters>
          <Counter name="One"  NormalSeverity="10000000" LowSeverity="20000000" HighSeverity="30000000" Value="{$sumAverageTransactionsTTRating2}" IsPrimary="True" >
            <Alarm>
              <xsl:attribute name="message" select="'Rating2 Count Average Transactions Sent is {0}'" />

              <Parameter>
                <xsl:value-of select="$sumAverageTransactionsTTRating2" />
              </Parameter>
            </Alarm>
          </Counter>
          
          <Counter name="Two"  NormalSeverity="83" LowSeverity="68" HighSeverity="0" Value="{$percentBalanseTT}" >
            <Alarm>
              <xsl:attribute name="message" select="'% Transactions Sent is {0}'" />

              <Parameter>
                <xsl:value-of select="$percentBalanseTT" />
              </Parameter>
            </Alarm>
          </Counter>
        </Counters>
      </TimesTenTransactionsRating2>
      
      <UDAGSessionsBalance IsDisabled="False">
        <xsl:variable name="udagModulesSchema" select="Schemas/Schema[@name = 'rating Module Oracle Session Statistics']/ModuleSessions/RawData/Modules/Module[@complexName = 'UDAG']" />
        <xsl:variable name="sumTotalSessionsAll" select="sum($udagModulesSchema/User/Instance/TotalSessions)" />
        <xsl:variable name="sumTotalSessionsRating1" select="sum($udagModulesSchema/User/Instance[@name = 'rating/rating1']/TotalSessions)" />
        <xsl:variable name="sumTotalSessionsRating2" select="sum($udagModulesSchema/User/Instance[@name = 'rating/rating2']/TotalSessions)" />
        <xsl:variable name="percentBalanseRating1" select="(100 div $sumTotalSessionsAll) * $sumTotalSessionsRating1" />
        <xsl:variable name="percentBalanseRating2" select="(100 div $sumTotalSessionsAll) * $sumTotalSessionsRating2" />
        <xsl:variable name="percentBalase" select="round-half-to-even(100 - (abs($percentBalanseRating1 - $percentBalanseRating2)),2)" />
        <Counters>
          <Counter name="One"  NormalSeverity="83" LowSeverity="68" HighSeverity="0" Value="{$percentBalase}" IsPrimary="True" >
            <Alarm>
              <xsl:attribute name="message" select="'% UDAG Sessions Balance is {0}'" />

              <Parameter>
                <xsl:value-of select="$percentBalase" />
              </Parameter>
            </Alarm>
          </Counter>
        </Counters>
      </UDAGSessionsBalance>
      
      <UDAGTotalSessionsRating1 IsDisabled="False">
        <xsl:variable name="udagModulesSchema" select="Schemas/Schema[@name = 'rating Module Oracle Session Statistics']/ModuleSessions/RawData/Modules/Module[@complexName = 'UDAG']" />
        <xsl:variable name="sumTotalSessionsAll" select="sum($udagModulesSchema/User/Instance/TotalSessions)" />
        <xsl:variable name="sumTotalSessionsRating1" select="sum($udagModulesSchema/User/Instance[@name = 'rating/rating1']/TotalSessions)" />
        <xsl:variable name="sumTotalSessionsRating2" select="sum($udagModulesSchema/User/Instance[@name = 'rating/rating2']/TotalSessions)" />
        <xsl:variable name="percentBalanseRating1" select="(100 div $sumTotalSessionsAll) * $sumTotalSessionsRating1" />
        <xsl:variable name="percentBalanseRating2" select="(100 div $sumTotalSessionsAll) * $sumTotalSessionsRating2" />
        <xsl:variable name="percentBalase" select="round-half-to-even(100 - (abs($percentBalanseRating1 - $percentBalanseRating2)),2)" />
        <Counters>
          <Counter name="One"  NormalSeverity="10000000" LowSeverity="20000000" HighSeverity="30000000" Value="{$sumTotalSessionsRating1}" IsPrimary="True" >
            <Alarm>
              <xsl:attribute name="message" select="'Total Sessions rating1 is {0}'" />

              <Parameter>
                <xsl:value-of select="$sumTotalSessionsRating1" />
              </Parameter>
            </Alarm>
          </Counter>
          <Counter name="Two"  NormalSeverity="83" LowSeverity="68" HighSeverity="0" Value="{$percentBalase}" >
            <Alarm>
              <xsl:attribute name="message" select="'% UDAG Sessions Balance is {0}'" />

              <Parameter>
                <xsl:value-of select="$percentBalase" />
              </Parameter>
            </Alarm>
          </Counter>
        </Counters>
      </UDAGTotalSessionsRating1>
      
      <UDAGTotalSessionsRating2 IsDisabled="False">
        <xsl:variable name="udagModulesSchema" select="Schemas/Schema[@name = 'rating Module Oracle Session Statistics']/ModuleSessions/RawData/Modules/Module[@complexName = 'UDAG']" />
        <xsl:variable name="sumTotalSessionsAll" select="sum($udagModulesSchema/User/Instance/TotalSessions)" />
        <xsl:variable name="sumTotalSessionsRating1" select="sum($udagModulesSchema/User/Instance[@name = 'rating/rating1']/TotalSessions)" />
        <xsl:variable name="sumTotalSessionsRating2" select="sum($udagModulesSchema/User/Instance[@name = 'rating/rating2']/TotalSessions)" />
        <xsl:variable name="percentBalanseRating1" select="(100 div $sumTotalSessionsAll) * $sumTotalSessionsRating1" />
        <xsl:variable name="percentBalanseRating2" select="(100 div $sumTotalSessionsAll) * $sumTotalSessionsRating2" />
        <xsl:variable name="percentBalase" select="round-half-to-even(100 - (abs($percentBalanseRating1 - $percentBalanseRating2)),2)" />
        <Counters>
          <Counter name="One"  NormalSeverity="10000000" LowSeverity="20000000" HighSeverity="30000000" Value="{$sumTotalSessionsRating2}" IsPrimary="True" >
            <Alarm>
              <xsl:attribute name="message" select="'Total Sessions rating2 is {0}'" />

              <Parameter>
                <xsl:value-of select="$sumTotalSessionsRating2" />
              </Parameter>
            </Alarm>
          </Counter>
          <Counter name="Two"  NormalSeverity="83" LowSeverity="68" HighSeverity="0" Value="{$percentBalase}" >
            <Alarm>
              <xsl:attribute name="message" select="'% UDAG Sessions Balance is {0}'" />

              <Parameter>
                <xsl:value-of select="$percentBalase" />
              </Parameter>
            </Alarm>
          </Counter>
        </Counters>
      </UDAGTotalSessionsRating2>
      
      <PostProcessing>
        <xsl:variable name="post_ready" select="Schemas/Schema[@name = 'rating Database Queue Statistic']/DatabaseQueueStatistic/RawData/Queues/Queue[@name = 'post_queue']" />
        <xsl:variable name="charge_signalling_queue" select="Schemas/Schema[@name = 'rating Database Queue Statistic']/DatabaseQueueStatistic/RawData/Queues/Queue[@name = 'charge_signalling_queue']" />
        <PostReadyQueue IsDisabled="False" name="{$post_ready/@name}">
          <Counters>
            <Counter name="One"  NormalSeverity="0" LowSeverity="50" HighSeverity="75" Value="{$post_ready/ReadyMessages}" IsPrimary="True" >
              <Alarm>
                <xsl:attribute name="message" select="'Post Ready Queue is {0}'" />

                <Parameter>
                  <xsl:value-of select="$post_ready/ReadyMessages" />
                </Parameter>
              </Alarm>
            </Counter>
          </Counters>
        </PostReadyQueue>
        <PostProcessedQueue IsDisabled="False" name="{$post_ready/@name}">
          <Counters>
            <Counter name="One"  NormalSeverity="0" LowSeverity="50" HighSeverity="75" Value="{$post_ready/ProcessedMessages}" IsPrimary="True" >
              <Alarm>
                <xsl:attribute name="message" select="'Post Processed Queue is {0}'" />

                <Parameter>
                  <xsl:value-of select="$post_ready/ProcessedMessages" />
                </Parameter>
              </Alarm>
            </Counter>
          </Counters>
        </PostProcessedQueue>
        <PostAverageTimeQueue IsDisabled="False" name="{$post_ready/@name}">
          <Counters>
            <Counter name="One"  NormalSeverity="0" LowSeverity="50" HighSeverity="75" Value="{$post_ready/AverageTime}" IsPrimary="True" >
              <Alarm>
                <xsl:attribute name="message" select="'Post Average Time Queue is {0}'" />

                <Parameter>
                  <xsl:value-of select="$post_ready/AverageTime" />
                </Parameter>
              </Alarm>
            </Counter>
          </Counters>
        </PostAverageTimeQueue>
        <PostTotalTimeQueue IsDisabled="False" name="{$post_ready/@name}">
          <Counters>
            <Counter name="One"  NormalSeverity="0" LowSeverity="50" HighSeverity="75" Value="{$post_ready/TotalTime}" IsPrimary="True" >
              <Alarm>
                <xsl:attribute name="message" select="'Post Total Time Queue is {0}'" />

                <Parameter>
                  <xsl:value-of select="$post_ready/TotalTime" />
                </Parameter>
              </Alarm>
            </Counter>
          </Counters>
        </PostTotalTimeQueue>
        <ChargeSignallingReadyQueue IsDisabled="False" name="{$charge_signalling_queue/@name}">
          <Counters>
            <Counter name="One"  NormalSeverity="0" LowSeverity="5000" HighSeverity="10000" Value="{$charge_signalling_queue/ReadyMessages}" IsPrimary="True" >
              <Alarm>
                <xsl:attribute name="message" select="'Charge Signalling Ready Queue is {0}'" />

                <Parameter>
                  <xsl:value-of select="$charge_signalling_queue/ReadyMessages" />
                </Parameter>
              </Alarm>
            </Counter>
          </Counters>
        </ChargeSignallingReadyQueue>
        <ChargeSignallingProcessedQueue IsDisabled="False" name="{$charge_signalling_queue/@name}">
          <Counters>
            <Counter name="One"  NormalSeverity="0" LowSeverity="5000" HighSeverity="10000" Value="{$charge_signalling_queue/ProcessedMessages}" IsPrimary="True" >
              <Alarm>
                <xsl:attribute name="message" select="'Charge Signalling Processed Queue is {0}'" />

                <Parameter>
                  <xsl:value-of select="$charge_signalling_queue/ProcessedMessages" />
                </Parameter>
              </Alarm>
            </Counter>
          </Counters>
        </ChargeSignallingProcessedQueue>
        <ChargeSignallingAverageTimeQueue IsDisabled="False" name="{$charge_signalling_queue/@name}">
          <Counters>
            <Counter name="One"  NormalSeverity="0" LowSeverity="5000" HighSeverity="10000" Value="{$charge_signalling_queue/AverageTime}" IsPrimary="True" >
              <Alarm>
                <xsl:attribute name="message" select="'Charge Signalling Average Time Queue is {0}'" />

                <Parameter>
                  <xsl:value-of select="$charge_signalling_queue/AverageTime" />
                </Parameter>
              </Alarm>
            </Counter>
          </Counters>
        </ChargeSignallingAverageTimeQueue>
        <ChargeSignallingTotalTimeQueue IsDisabled="False" name="{$charge_signalling_queue/@name}">
          <Counters>
            <Counter name="One"  NormalSeverity="0" LowSeverity="5000" HighSeverity="10000" Value="{$charge_signalling_queue/TotalTime}" IsPrimary="True" >
              <Alarm>
                <xsl:attribute name="message" select="'Charge Signalling Total Time Queue is {0}'" />

                <Parameter>
                  <xsl:value-of select="$charge_signalling_queue/TotalTime" />
                </Parameter>
              </Alarm>
            </Counter>
          </Counters>
        </ChargeSignallingTotalTimeQueue>
        <LinkPostReadyQueueGraphicData IsDisabled="False">
          <xsl:copy-of select="$post_ready/GraphicData/Value" />
        </LinkPostReadyQueueGraphicData>
        <LinkChargeSignallingQueueGraphicData IsDisabled="False">
          <xsl:copy-of select="$charge_signalling_queue/GraphicData/Value" />
        </LinkChargeSignallingQueueGraphicData>
      </PostProcessing>
      
      <ChargeProcessing>
        <xsl:variable name="charge_processing_queue" select="Schemas/Schema[@name = 'charging Database Queue Statistic']/DatabaseQueueStatistic/RawData/Queues/Queue[@name = 'charge_processing_queue']" />
        <ChargeProcessingReadyQueue IsDisabled="False" name="{$charge_processing_queue/@name}">
          <Counters>
            <Counter name="One"  NormalSeverity="0" LowSeverity="5000" HighSeverity="10000" Value="{$charge_processing_queue/ReadyMessages}" IsPrimary="True" >
              <Alarm>
                <xsl:attribute name="message" select="'Charge Processing Ready Queue is {0}'" />

                <Parameter>
                  <xsl:value-of select="$charge_processing_queue/ReadyMessages" />
                </Parameter>
              </Alarm>
            </Counter>
          </Counters>
        </ChargeProcessingReadyQueue>
        <ChargeProcessingProcessedQueue IsDisabled="False" name="{$charge_processing_queue/@name}">
          <Counters>
            <Counter name="One"  NormalSeverity="0" LowSeverity="5000" HighSeverity="10000" Value="{$charge_processing_queue/ProcessedMessages}" IsPrimary="True" >
              <Alarm>
                <xsl:attribute name="message" select="'Charge Processing Processed Queue is {0}'" />

                <Parameter>
                  <xsl:value-of select="$charge_processing_queue/ProcessedMessages" />
                </Parameter>
              </Alarm>
            </Counter>
          </Counters>
        </ChargeProcessingProcessedQueue>
        <ChargeProcessingAverageTimeQueue IsDisabled="False" name="{$charge_processing_queue/@name}">
          <Counters>
            <Counter name="One"  NormalSeverity="0" LowSeverity="5000" HighSeverity="10000" Value="{$charge_processing_queue/AverageTime}" IsPrimary="True" >
              <Alarm>
                <xsl:attribute name="message" select="'Charge Processing Average Time Queue is {0}'" />

                <Parameter>
                  <xsl:value-of select="$charge_processing_queue/AverageTime" />
                </Parameter>
              </Alarm>
            </Counter>
          </Counters>
        </ChargeProcessingAverageTimeQueue>
        <ChargeProcessingTotalTimeQueue IsDisabled="False" name="{$charge_processing_queue/@name}">
          <Counters>
            <Counter name="One"  NormalSeverity="0" LowSeverity="5000" HighSeverity="10000" Value="{$charge_processing_queue/TotalTime}" IsPrimary="True" >
              <Alarm>
                <xsl:attribute name="message" select="'Charge Processing Total Time Queue is {0}'" />

                <Parameter>
                  <xsl:value-of select="$charge_processing_queue/TotalTime" />
                </Parameter>
              </Alarm>
            </Counter>
          </Counters>
        </ChargeProcessingTotalTimeQueue>
        <LinkChargeProcessingQueueGraphicData IsDisabled="False">
          <xsl:copy-of select="$charge_processing_queue/GraphicData/Value" />
        </LinkChargeProcessingQueueGraphicData>
      </ChargeProcessing>
      
      <RatingJobs IsDisabled="False">
        <xsl:variable name="jobs" select="Schemas/Schema[@name = 'rating Jobs Group Statistics Schema']/JobsGroupsStatistic/RawData" />
        <xsl:variable name="countJobs" >
          <xsl:choose>
            <xsl:when test="string-length($jobs/Databases/Database/TotalJobs) = 0" >
              <xsl:value-of select="sum($jobs/Instances/Instance/TotalJobs)" />
            </xsl:when>
            <xsl:otherwise>
              <xsl:value-of select="$jobs/Databases/Database/TotalJobs + sum($jobs/Instances/Instance/TotalJobs)" />
            </xsl:otherwise>
          </xsl:choose>
        </xsl:variable>
        <Counters>
          <Counter name="One" NormalSeverity="1" HighSeverity="0" Value="{$countJobs}" IsPrimary="True" >
            <Alarm>
              <xsl:attribute name="message" select="'Count Rating Jobs is {0}'" />

              <Parameter>
                <xsl:value-of select="$countJobs" />
              </Parameter>
            </Alarm>
          </Counter>
        </Counters>
      </RatingJobs>
      
      <ChargingJobs IsDisabled="False">
        <xsl:variable name="jobs" select="Schemas/Schema[@name = 'charging Jobs Group Statistics Schema']/JobsGroupsStatistic/RawData" />
        <xsl:variable name="countJobs" >
          <xsl:choose>
            <xsl:when test="string-length($jobs/Databases/Database/TotalJobs) = 0" >
              <xsl:value-of select="sum($jobs/Instances/Instance/TotalJobs)" />
            </xsl:when>
            <xsl:otherwise>
              <xsl:value-of select="$jobs/Databases/Database/TotalJobs + sum($jobs/Instances/Instance/TotalJobs)" />
            </xsl:otherwise>
          </xsl:choose>
        </xsl:variable>
        <Counters>
          <Counter name="One" NormalSeverity="2" HighSeverity="0" Value="{$countJobs}" IsPrimary="True" >
            <Alarm>
              <xsl:attribute name="message" select="'Count Charging Jobs is {0}'" />

              <Parameter>
                <xsl:value-of select="$countJobs" />
              </Parameter>
            </Alarm>
          </Counter>
        </Counters>
      </ChargingJobs>
      
      <QueuePropagationStatistics IsDisabled="False">
        <xsl:variable name="averageNumber" select="Schemas/Schema[@name = 'rating Propagation Statistics']/QueuePropagationStatistic/RawData/Propagations/Propagation[@databaseName = 'rating']/AverageNumber" />
        <Counters>
          <Counter name="One" NormalSeverity="10000" LowSeverity="14000" HighSeverity="15000" Value="{$averageNumber}" IsPrimary="True" >
            <Alarm>
              <xsl:attribute name="message" select="'Queue Propagation is {0}'" />

              <Parameter>
                <xsl:value-of select="$averageNumber" />
              </Parameter>
            </Alarm>
          </Counter>
        </Counters>
      </QueuePropagationStatistics>
      
      <LinkAverageNumberQueuePropagationGraphicData IsDisabled="False">
        <xsl:variable name="rating" select="Schemas/Schema[@name = 'rating Propagation Statistics']/QueuePropagationStatistic/RawData/Propagations/Propagation[@databaseName = 'rating']/GraphicData" />
        <xsl:copy-of select="$rating/Value" />
      </LinkAverageNumberQueuePropagationGraphicData>
      
      <Cooler_172_27_129_132>
        <Counters>
          <Counter name="Total Processor Load" NormalSeverity="0" LowSeverity="70" HighSeverity="80" Value="{Schemas/Schema[@name = '172.27.129.132 Device Snapshot']/DeviceSnapshot/RawData/Processors/Processor[@id = 'Total']/CPUUsage}" IsPrimary="True" >
            <Alarm>
              <xsl:attribute name="message" select="'Total Processor usage is {0}'" />
              <Parameter>
                <xsl:value-of select="Schemas/Schema[@name = '172.27.129.132 Device Snapshot']/DeviceSnapshot/RawData/Processors/Processor[@id = 'Total']/CPUUsage" />
              </Parameter>
            </Alarm>
          </Counter>
          <xsl:for-each select="Schemas/Schema[@name = '172.27.129.132 Device Snapshot']/DeviceSnapshot/RawData/Storages/Storage[@name = 'Physical memory' or @name = 'Swap space' or @name = 'Virtual memory' or @name = '/var' or @name = '/usr' or @name = '/' or @name = '/opt' or @name = '/globaldevices']" >
            <xsl:variable name="name" select="concat('Storage ',@name)" />
            <xsl:variable name="freeSpaceMBytes" select="FreeSpace[@counter = 'MBytes']" />
            <xsl:variable name="freeSpacePercent" select="FreeSpace[@counter = 'Percent']" />
            <xsl:variable name="status" >
              <xsl:choose>
                <xsl:when test="$freeSpaceMBytes &gt;= 2048 or $freeSpacePercent &gt; 20">
                  <xsl:text>0</xsl:text>
                </xsl:when>
                <xsl:when test="($freeSpaceMBytes &lt;= 2048 or $freeSpacePercent &lt;= 20) and ($freeSpaceMBytes &gt; 1024 or $freeSpacePercent &gt; 10)">
                  <xsl:text>1</xsl:text>
                </xsl:when>
                <xsl:when test="$freeSpaceMBytes &lt;= 1048 or $freeSpacePercent &lt;= 10">
                  <xsl:text>2</xsl:text>
                </xsl:when>
              </xsl:choose>
            </xsl:variable>
            <Counter name="{$name}" NormalSeverity="0" LowSeverity="1" HighSeverity="2" Value="{$status}" >
              <Alarm>
                <xsl:attribute name="message" select="concat($name,' Free space is {0} MBytes {1} %')" />
                <Parameter>
                  <xsl:value-of select="$freeSpaceMBytes" />
                </Parameter>
                <Parameter>
                  <xsl:value-of select="$freeSpacePercent" />
                </Parameter>
              </Alarm>
            </Counter>
          </xsl:for-each>
          <xsl:for-each select="Schemas/Schema[@name = '172.27.129.132 Disks Usage']/DisksData/RawData/Disks/Disk" >
            <xsl:variable name="name" select="concat('Disk ',@name)" />
            <xsl:variable name="avgWaitTime" select="AvgWaitTime" />
            <xsl:variable name="avgServicingTime" select="AvgServicingTime" />
            <Counter name="{$name}" NormalSeverity="0" HighSeverity="90" Value="{$avgWaitTime}" >
              <Alarm>
                <xsl:attribute name="message" select="concat($name,' Avg Wait Time is {0}')" />
                <Parameter>
                  <xsl:value-of select="$avgWaitTime" />
                </Parameter>
              </Alarm>
            </Counter>
            <Counter name="{$name}" NormalSeverity="0" HighSeverity="50" Value="{$avgServicingTime}" >
              <Alarm>
                <xsl:attribute name="message" select="concat($name,' Avg Servicing Time is {0}')" />
                <Parameter>
                  <xsl:value-of select="$avgServicingTime" />
                </Parameter>
              </Alarm>
            </Counter>
          </xsl:for-each>
        </Counters>
      </Cooler_172_27_129_132>
      
      <Cooler_172_27_129_133>
        <Counters>
          <Counter name="Total Processor Load" NormalSeverity="0" LowSeverity="70" HighSeverity="80" Value="{Schemas/Schema[@name = '172.27.129.133 Device Snapshot']/DeviceSnapshot/RawData/Processors/Processor[@id = 'Total']/CPUUsage}" IsPrimary="True" >
            <Alarm>
              <xsl:attribute name="message" select="'Total Processor usage is {0}'" />
              <Parameter>
                <xsl:value-of select="Schemas/Schema[@name = '172.27.129.133 Device Snapshot']/DeviceSnapshot/RawData/Processors/Processor[@id = 'Total']/CPUUsage" />
              </Parameter>
            </Alarm>
          </Counter>
          <xsl:for-each select="Schemas/Schema[@name = '172.27.129.133 Device Snapshot']/DeviceSnapshot/RawData/Storages/Storage[@name = 'Physical memory' or @name = 'Swap space' or @name = 'Virtual memory' or @name = '/var' or @name = '/usr' or @name = '/' or @name = '/opt' or @name = '/globaldevices']" >
            <xsl:variable name="name" select="concat('Storage ',@name)" />
            <xsl:variable name="freeSpaceMBytes" select="FreeSpace[@counter = 'MBytes']" />
            <xsl:variable name="freeSpacePercent" select="FreeSpace[@counter = 'Percent']" />
            <xsl:variable name="status" >
              <xsl:choose>
                <xsl:when test="$freeSpaceMBytes &gt;= 2048 or $freeSpacePercent &gt; 20">
                  <xsl:text>0</xsl:text>
                </xsl:when>
                <xsl:when test="($freeSpaceMBytes &lt;= 2048 or $freeSpacePercent &lt;= 20) and ($freeSpaceMBytes &gt; 1024 or $freeSpacePercent &gt; 10)">
                  <xsl:text>1</xsl:text>
                </xsl:when>
                <xsl:when test="$freeSpaceMBytes &lt;= 1048 or $freeSpacePercent &lt;= 10">
                  <xsl:text>2</xsl:text>
                </xsl:when>
              </xsl:choose>
            </xsl:variable>
            <Counter name="{$name}" NormalSeverity="0" LowSeverity="1" HighSeverity="2" Value="{$status}" >
              <Alarm>
                <xsl:attribute name="message" select="concat($name,' Free space is {0} MBytes {1} %')" />
                <Parameter>
                  <xsl:value-of select="$freeSpaceMBytes" />
                </Parameter>
                <Parameter>
                  <xsl:value-of select="$freeSpacePercent" />
                </Parameter>
              </Alarm>
            </Counter>
          </xsl:for-each>
          <xsl:for-each select="Schemas/Schema[@name = '172.27.129.133 Disks Usage']/DisksData/RawData/Disks/Disk" >
            <xsl:variable name="name" select="concat('Disk ',@name)" />
            <xsl:variable name="avgWaitTime" select="AvgWaitTime" />
            <xsl:variable name="avgServicingTime" select="AvgServicingTime" />
            <Counter name="{$name}" NormalSeverity="0" HighSeverity="90" Value="{$avgWaitTime}" >
              <Alarm>
                <xsl:attribute name="message" select="concat($name,' Avg Wait Time is {0}')" />
                <Parameter>
                  <xsl:value-of select="$avgWaitTime" />
                </Parameter>
              </Alarm>
            </Counter>
            <Counter name="{$name}" NormalSeverity="0" HighSeverity="50" Value="{$avgServicingTime}" >
              <Alarm>
                <xsl:attribute name="message" select="concat($name,' Avg Servicing Time is {0}')" />
                <Parameter>
                  <xsl:value-of select="$avgServicingTime" />
                </Parameter>
              </Alarm>
            </Counter>
          </xsl:for-each>
        </Counters>
      </Cooler_172_27_129_133>
      
      <Cooler_172_27_129_151>
        <Counters>
          <Counter name="Total Processor Load" NormalSeverity="0" LowSeverity="70" HighSeverity="80" Value="{Schemas/Schema[@name = '172.27.129.151 Device Snapshot']/DeviceSnapshot/RawData/Processors/Processor[@id = 'Total']/CPUUsage}" IsPrimary="True" >
            <Alarm>
              <xsl:attribute name="message" select="'Total Processor usage is {0}'" />
              <Parameter>
                <xsl:value-of select="Schemas/Schema[@name = '172.27.129.151 Device Snapshot']/DeviceSnapshot/RawData/Processors/Processor[@id = 'Total']/CPUUsage" />
              </Parameter>
            </Alarm>
          </Counter>
          <xsl:for-each select="Schemas/Schema[@name = '172.27.129.151 Device Snapshot']/DeviceSnapshot/RawData/Storages/Storage[@name = 'Physical memory' or @name = 'Swap space' or @name = 'Virtual memory' or @name = '/var' or @name = '/usr' or @name = '/' or @name = '/opt' or @name = '/globaldevices']" >
            <xsl:variable name="name" select="concat('Storage ',@name)" />
            <xsl:variable name="freeSpaceMBytes" select="FreeSpace[@counter = 'MBytes']" />
            <xsl:variable name="freeSpacePercent" select="FreeSpace[@counter = 'Percent']" />
            <xsl:variable name="status" >
              <xsl:choose>
                <xsl:when test="$freeSpaceMBytes &gt;= 2048 or $freeSpacePercent &gt; 20">
                  <xsl:text>0</xsl:text>
                </xsl:when>
                <xsl:when test="($freeSpaceMBytes &lt;= 2048 or $freeSpacePercent &lt;= 20) and ($freeSpaceMBytes &gt; 1024 or $freeSpacePercent &gt; 10)">
                  <xsl:text>1</xsl:text>
                </xsl:when>
                <xsl:when test="$freeSpaceMBytes &lt;= 1048 or $freeSpacePercent &lt;= 10">
                  <xsl:text>2</xsl:text>
                </xsl:when>
              </xsl:choose>
            </xsl:variable>
            <Counter name="{$name}" NormalSeverity="0" LowSeverity="1" HighSeverity="2" Value="{$status}" >
              <Alarm>
                <xsl:attribute name="message" select="concat($name,' Free space is {0} MBytes {1} %')" />
                <Parameter>
                  <xsl:value-of select="$freeSpaceMBytes" />
                </Parameter>
                <Parameter>
                  <xsl:value-of select="$freeSpacePercent" />
                </Parameter>
              </Alarm>
            </Counter>
          </xsl:for-each>
          <xsl:for-each select="Schemas/Schema[@name = '172.27.129.151 Disks Usage']/DisksData/RawData/Disks/Disk" >
            <xsl:variable name="name" select="concat('Disk ',@name)" />
            <xsl:variable name="avgWaitTime" select="AvgWaitTime" />
            <xsl:variable name="avgServicingTime" select="AvgServicingTime" />
            <Counter name="{$name}" NormalSeverity="0" HighSeverity="90" Value="{$avgWaitTime}" >
              <Alarm>
                <xsl:attribute name="message" select="concat($name,' Avg Wait Time is {0}')" />
                <Parameter>
                  <xsl:value-of select="$avgWaitTime" />
                </Parameter>
              </Alarm>
            </Counter>
            <Counter name="{$name}" NormalSeverity="0" HighSeverity="50" Value="{$avgServicingTime}" >
              <Alarm>
                <xsl:attribute name="message" select="concat($name,' Avg Servicing Time is {0}')" />
                <Parameter>
                  <xsl:value-of select="$avgServicingTime" />
                </Parameter>
              </Alarm>
            </Counter>
          </xsl:for-each>
        </Counters>
      </Cooler_172_27_129_151>
      
      <GraphicsStrokes IsDisabled="False">
        <xsl:variable name="totalStrokes" select="Schemas/Schema[@name = 'Rating Installation 1 Snapshot']/RatingSnapshot/RawData/Strokes/GraphicData/Graphic[@name = 'Total Strokes']" />
        <xsl:copy-of select="$totalStrokes" />
      </GraphicsStrokes>
  
      <GraphicsRatingGarbages IsDisabled="False">
        <xsl:variable name="totalStrokes" select="Schemas/Schema[@name = 'Rating Installation 1 Snapshot']/RatingSnapshot/RawData/Garbages/GraphicData/Graphic" />
        <xsl:copy-of select="$totalStrokes" />
      </GraphicsRatingGarbages>
    </TransformedSchemas>
  </xsl:template>
</xsl:stylesheet>