#tool "nuget:?package=xunit.runner.console"

var target = Argument( "target", "Default" );
var configuration = Argument( "configuration", "Release" );
var tests = Argument( "tests", "All" );

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

Task( "RunUITests" )
   .IsDependentOn( "Build" )
   .Does( () =>
{
   if ( tests == "All" )
   {
      XUnit2( "./SoundButton/SoundButton.UITests/bin/" + Directory( configuration ) + "/*Tests*.dll" );
   }
   else
   {
      Information( "UI Tests Skipped for this configuration" );
   }    
} );

Task( "RunAllTests" )
   .IsDependentOn( "RunUnitTests" )
   .IsDependentOn( "RunUITests" );

//===========================================================================
// Default Task
//===========================================================================

Task( "Default" )
   .IsDependentOn( "RunAllTests" );

RunTarget( target );
