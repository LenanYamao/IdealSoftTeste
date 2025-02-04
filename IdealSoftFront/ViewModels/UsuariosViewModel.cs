using IdealSoftFront.Helpers;
using IdealSoftFront.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Input;

namespace IdealSoftFront.ViewModels
{
    public class UsuariosViewModel : INotifyPropertyChanged
    {
        private readonly HttpClient _httpClient;
        private Usuario? _selectedUsuario;
        private Usuario _newUsuario = new Usuario();

        // An ObservableCollection to automatically update the UI when items change.
        public ObservableCollection<Usuario> Usuarios { get; } = new ObservableCollection<Usuario>();

        public Usuario? SelectedUsuario
        {
            get => _selectedUsuario;
            set
            {
                if (_selectedUsuario != value)
                {
                    _selectedUsuario = value;
                    OnPropertyChanged(nameof(SelectedUsuario));
                }
            }
        }
        public Usuario NewUsuario
        {
            get => _newUsuario;
            set
            {
                if (_newUsuario != value)
                {
                    _newUsuario = value;
                    OnPropertyChanged(nameof(NewUsuario));
                }
            }
        }
        public ICommand CreateCommand { get; }
        public ICommand UpdateCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand RefreshCommand { get; }

        public UsuariosViewModel()
        {
            _httpClient = new HttpClient();

            _httpClient.BaseAddress = new Uri("http://localhost:5110/");

            // Initialize commands.
            CreateCommand = new RelayCommand(async _ => await CreateUsuarioAsync(), _ => NewUsuario != null && !string.IsNullOrWhiteSpace(NewUsuario.Nome));
            UpdateCommand = new RelayCommand(async _ => await UpdateUsuarioAsync(), _ => SelectedUsuario != null);
            DeleteCommand = new RelayCommand(async _ => await DeleteUsuarioAsync(), _ => SelectedUsuario != null);
            RefreshCommand = new RelayCommand(async _ => await LoadUsuariosAsync());

            _ = LoadUsuariosAsync();
        }

        public async Task LoadUsuariosAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("/Usuario/GetUsuarios");
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                var items = JsonSerializer.Deserialize<ObservableCollection<Usuario>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (items != null)
                {
                    Usuarios.Clear();
                    foreach (var item in items)
                    {
                        Usuarios.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error loading usuarios: " + ex.Message);
            }
        }

        public async Task CreateUsuarioAsync()
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("/Usuario/PostUsuario", NewUsuario);
                response.EnsureSuccessStatusCode();

                var createdItem = await response.Content.ReadFromJsonAsync<Usuario>();
                if (createdItem != null)
                {
                    Usuarios.Add(createdItem);
                    NewUsuario = new Usuario();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error creating usuario: " + ex.Message);
            }
        }

        public async Task UpdateUsuarioAsync()
        {
            if (SelectedUsuario == null) return;
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"/Usuario/PutUsuario/{SelectedUsuario.Id}", SelectedUsuario);
                response.EnsureSuccessStatusCode();
                // Optionally, refresh the list or update properties accordingly.
                await LoadUsuariosAsync();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error updating usuario: " + ex.Message);
            }
        }

        public async Task DeleteUsuarioAsync()
        {
            if (SelectedUsuario == null) return;
            try
            {
                var response = await _httpClient.DeleteAsync($"/Usuario/DeleteUsuario/{SelectedUsuario.Id}");
                response.EnsureSuccessStatusCode();
                Usuarios.Remove(SelectedUsuario);
                SelectedUsuario = null;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error deleting usuario: " + ex.Message);
            }
        }

        #region INotifyPropertyChanged Implementation

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
