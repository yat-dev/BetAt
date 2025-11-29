// Global using directives

global using Microsoft.AspNetCore.Mvc;
global using BetAt.Application.Features.Users.Queries;
global using BetAt.Domain.Entities;
global using MediatR;
global using Microsoft.AspNetCore.Authorization;
global using BetAt.Application.Dtos;
global using BetAt.Application.Features.Bet.Queries;
global using BetAt.Application.Dtos.Leagues;
global using System.Text;
global using BetAt.API.Middleware;
global using BetAt.Application;
global using BetAt.Infrastructure;
global using Microsoft.AspNetCore.Authentication.JwtBearer;
global using Microsoft.IdentityModel.Tokens;
global using Microsoft.OpenApi.Models;
global using BetAt.API.Models;
global using Serilog;
global using Serilog.Events;
global using Serilog.Exceptions;
global using Serilog.Formatting.Json;
global using BetAt.Application.Features.Auth.Commands;

