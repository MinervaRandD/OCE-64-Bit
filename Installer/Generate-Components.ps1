param(
    [string]$SourceDir = "C:\Minerva Research and Development\Projects\OCE-64-Bit\FloorMaterialEstimator\FloorMaterialEstimatorRev2\bin\x64\Debug",
    [string]$OutputPath = "Components.wxs"
)

$fullSourceDir = $SourceDir

# Basic WiX 4 fragment header
$xmlHeader = @"
<?xml version="1.0" encoding="utf-8"?>
<Wix xmlns="http://wixtoolset.org/schemas/v4/wxs">
  <Fragment>
    <DirectoryRef Id="INSTALLFOLDER">
"@

$xmlFooter = @"
    </DirectoryRef>

    <ComponentGroup Id="OCEFME_Components">
"@

$xmlFooter2 = @"
    </ComponentGroup>
  </Fragment>
</Wix>
"@

$components = New-Object System.Collections.Generic.List[string]
$groupRefs  = New-Object System.Collections.Generic.List[string]

# One component per file under INSTALLFOLDER
Get-ChildItem -Path $SourceDir -File | ForEach-Object {
    $file = $_
    $componentId = ("Cmp_" + ($file.Name -replace '[^\w]', '_'))
    $fileId      = ("Fil_" + ($file.Name -replace '[^\w]', '_'))
    $guid        = [guid]::NewGuid().ToString()
    $sourcePath  = "$fullSourceDir\$($file.Name)"

    $componentXml = @"
      <Component Id="$componentId" Guid="$guid">
        <File Id="$fileId"
              Source="$sourcePath"
              KeyPath="yes" />
      </Component>
"@

    $components.Add($componentXml)
    $groupRefs.Add("      <ComponentRef Id=""$componentId"" />")
}

# Write out Components.wxs
$sb = New-Object System.Text.StringBuilder
$null = $sb.AppendLine($xmlHeader)
foreach ($c in $components) { $null = $sb.Append($c) }
$null = $sb.AppendLine($xmlFooter)
foreach ($g in $groupRefs) { $null = $sb.AppendLine($g) }
$null = $sb.AppendLine($xmlFooter2)

Set-Content -Path $OutputPath -Value $sb.ToString() -Encoding UTF8

Write-Host "Generated $OutputPath from $fullSourceDir"
