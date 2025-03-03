// Project: AOKMovieLibrary

global using MovieLibraryDataServer.Abstractions;
global using MovieLibraryDataServer.Implementations;
global using MovieLibraryDataServer.Models.Commands;
global using MovieLibraryDataServer.Models.DAL;
global using MovieLibraryDataServer.Models.Responses;
global using MovieLibraryDataServer.Models.ViewModels;
global using MovieLibraryDataServer.ProjectConfiguration;

// Microsoft

global using Microsoft.AspNetCore.Components;
global using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
global using Microsoft.Extensions.DependencyInjection;
global using System.ComponentModel.DataAnnotations;

// Misc
global using FluentValidation;

[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("AOKMovieLibrary.Tests")]