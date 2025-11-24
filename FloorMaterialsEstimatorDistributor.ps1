Set-Location "C:\Minerva Research and Development\Projects\OCERev4\FloorMaterialEstimator\FloorMaterialEstimatorRev2\bin"

# Build the filename with today's date (yyyy-MM-dd)
$today = Get-Date -Format "yyyy-MM-dd"
$zipName = "FloorMaterialsEstimator$today.zip"

Compress-Archive -Path ".\Debug" -DestinationPath ".\$zipName" -Force

Move-Item ".\$zipName" "C:\Temp\" -Force