@echo off
@echo.
TMU.exe -all src\EPiServer.Marketing.KPI\EmbeddedLangFiles\Episerver_Kpi_EN.xml src\EPiServer.Marketing.KPI\EmbeddedLangFiles\ src\EPiServer.Marketing.KPI\EmbeddedLangDiffs\
TMU.exe -all src\EPiServer.Marketing.KPI.Commerce\EmbeddedLangFiles\EpiServer_Kpi_Commerce_EN.xml src\EPiServer.Marketing.KPI.Commerce\EmbeddedLangFiles\ src\EPiServer.Marketing.KPI.Commerce\EmbeddedLangDiffs\
TMU.exe -all src\EPiServer.Marketing.Testing.Web\EmbeddedLangFiles\EpiServer_Testing_EN.xml src\EPiServer.Marketing.Testing.Web\EmbeddedLangFiles\ src\EPiServer.Marketing.Testing.Web\EmbeddedLangDiffs\
