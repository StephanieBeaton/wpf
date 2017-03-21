USE [Inventory]
GO

ALTER TABLE [dbo].[Item] DROP CONSTRAINT [DF_Item_ItemCreatedDate]
GO

/****** Object:  Table [dbo].[Item]    Script Date: 3/8/2017 2:49:23 PM ******/
DROP TABLE [dbo].[Item]
GO

/****** Object:  Table [dbo].[Item]    Script Date: 3/8/2017 2:49:23 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*
  • Item # (int)
  • Description
  • Price per item (double)
  • Quantity on hand (int)
  • Our cost per item (double)
  • Value of item (price * quantity on hand)
*/

CREATE TABLE [dbo].[Item](
	[ItemId] [int] IDENTITY(1,1) NOT NULL,
	[ItemDesc] [nvarchar](50) NOT NULL,
	[ItemPrice] [float] NOT NULL,
	[ItemQuantity] [int] NOT NULL,
	[ItemCost] [float] NOT NULL,
	[ItemValue]  AS ([ItemPrice]*[ItemQuantity]),
	[ItemCreatedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Item] PRIMARY KEY CLUSTERED 
(
	[ItemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, 
IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Item] ADD  CONSTRAINT [DF_Item_ItemCreatedDate]  DEFAULT (getdate()) FOR [ItemCreatedDate]
GO

