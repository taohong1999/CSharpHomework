<?xml version="1.0" encoding = "UTF-8"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">
	<xsl:template match="/">
		<html>
				<head>
						<title>Orders</title>
				</head>
				<body>
					<xsl:for-each select="OrderService/mOrders/Orders">
						<ul>
							<br/>
							<li>订单号：<xsl:value-of select="orderNumber" /></li>
							<li>客户姓名：<xsl:value-of select="theCustomer" /></li>
							<li>订单电话：<xsl:value-of select="phoneNumber" /></li>
							<li>订单总额：<xsl:value-of select="money" /></li>
							<br/>
							<li>订单明细</li>
							<xsl:for-each select="mOrderDetails/OrderDetails">
							<li>商品名称：<xsl:value-of select="goodName" /></li>
							<li>商品单价：<xsl:value-of select="goodPrice" /></li>
							<li>商品数量：<xsl:value-of select="goodNumber" /></li>
							<br/>
							<br/>
							</xsl:for-each>
						</ul>
					</xsl:for-each>
				</body>
		</html>
	</xsl:template>
</xsl:stylesheet>
							
							