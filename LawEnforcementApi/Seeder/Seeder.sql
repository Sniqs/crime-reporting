USE [CR_LawEnforcement]
GO
INSERT [dbo].[Ranks] ([Id], [Name]) VALUES (N'1685cae0-71e4-4e76-83a9-5c9488b3942f', N'Lieutenant')
INSERT [dbo].[Ranks] ([Id], [Name]) VALUES (N'8960db50-77bd-4ea1-9038-952eb09a4eb5', N'Sergeant')
INSERT [dbo].[Ranks] ([Id], [Name]) VALUES (N'ebb26b52-b7b9-42a2-8687-b06d1b2011ee', N'Deputy')
GO
INSERT [dbo].[Officers] ([Id], [CallSign], [OfficerRankId]) VALUES (N'01fde555-3de6-4ad2-ab32-ac725a6391e9', N'Tango-4', N'8960db50-77bd-4ea1-9038-952eb09a4eb5')
INSERT [dbo].[Officers] ([Id], [CallSign], [OfficerRankId]) VALUES (N'098603a4-895a-4b1e-85ee-bb83ed801c0a', N'Echo-1', N'1685cae0-71e4-4e76-83a9-5c9488b3942f')
INSERT [dbo].[Officers] ([Id], [CallSign], [OfficerRankId]) VALUES (N'b37e6518-d42b-4809-b848-c1501cf5af80', N'Charlie-10', N'ebb26b52-b7b9-42a2-8687-b06d1b2011ee')
GO
