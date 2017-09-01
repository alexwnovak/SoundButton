#tool "nuget:?package=xunit.runner.console"

var target = Argument( "target", "Default" );
var configuration = Argument( "configuration", "Release" );

var buildDir = Directory( "./SoundButton/SoundButton/bin" ) + Directory( configuration );

//===========================================================================
// Clean Task
//===========================================================================

Task( "Clean" )
   .Does( () =>
{
   CleanDirectory( buildDir );
});

//===========================================================================
// Restore Task
//===========================================================================

Task( "RestoreNuGetPackages" )
   .IsDependentOn( "Clean" )
   .Does( () =>
{
   NuGetRestore( "./SoundButton/SoundButton.sln" );
} );

//===========================================================================
// Build Task
//===========================================================================

Task( "Build" )
   .IsDependentOn( "RestoreNuGetPackages")
   .Does( () =>
{
  MSBuild( "./SoundButton/SoundButton.sln", settings => settings.SetConfiguration( configuration ) );
} );

//===========================================================================
// Test Task
//===========================================================================

Task( "RunUnitTests" )
   .IsDependentOn( "Build" )
   .Does( () =>
{
    XUnit2( "./SoundButton/SoundButton.UnitTests/bin/" + Directory( configuration ) + "/*Tests*.dll" );
} );

//===========================================================================
// Default Task
//===========================================================================

Task( "Default" )
   .IsDependentOn( "RunUnitTests" );

RunTarget( target );
