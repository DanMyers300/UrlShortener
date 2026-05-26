{
  description = "C# development environment";

  inputs = {
    nixpkgs.url = "github:NixOS/nixpkgs/nixos-unstable";
    flake-utils.url = "github:numtide/flake-utils";
  };

  outputs = { self, nixpkgs, flake-utils }:
    flake-utils.lib.eachDefaultSystem (system:
      let
        pkgs = nixpkgs.legacyPackages.${system};
      in {
        devShells.default = pkgs.mkShell {
          packages = with pkgs; [
            dotnet-sdk_9
            omnisharp-roslyn
            netcoredbg
            dotnet-ef
          ];

          env = {
            DOTNET_ROOT = "${pkgs.dotnet-sdk_9}";
            DOTNET_CLI_TELEMETRY_OPTOUT = "1";
          };
        };
      });
}
