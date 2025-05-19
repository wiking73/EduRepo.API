import React, { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import axios from 'axios';
import './Styles/Profile.css';

const UserProfile = () => {
    const navigate = useNavigate();
    const [profile, setProfile] = useState(null);
    const [error, setError] = useState(null);
    const [loading, setLoading] = useState(true);
    const [showChangePasswordForm, setShowChangePasswordForm] = useState(false);
    const [currentPassword, setCurrentPassword] = useState('');
    const [newPassword, setNewPassword] = useState('');
    const [changePasswordMessage, setChangePasswordMessage] = useState(null);
    const [changePasswordSuccess, setChangePasswordSuccess] = useState(null);

    const handleLogout = () => {
        localStorage.removeItem('authToken');
        localStorage.removeItem('displayName');
        localStorage.removeItem('role');
        navigate('/navbar');
    };

    const handlePasswordChange = async (e) => {
        e.preventDefault();
        setChangePasswordMessage(null);
        setChangePasswordSuccess(null);

        const token = localStorage.getItem('authToken');
        if (!token) {
            setChangePasswordMessage('Authorization token not found.');
            setChangePasswordSuccess(false);
            return;
        }

        try {
            await axios.post(
                'https://localhost:7157/api/Auth/change-password-login',
                {
                    currentPassword,
                    newPassword,
                },
                {
                    headers: { Authorization: `Bearer ${token}` },
                }
            );
            setChangePasswordMessage('Password changed successfully.');
            setChangePasswordSuccess(true);
            setCurrentPassword('');
            setNewPassword('');
        } catch (error) {
            setChangePasswordMessage(
                error.response?.data?.message || 'Error changing password.'
            );
            setChangePasswordSuccess(false);
            console.error(error);
        }
    };

    const handleDeleteAccount = async () => {
        const token = localStorage.getItem('authToken');
        if (!token) {
            setError('Authorization token not found.');
            return;
        }

        const confirmDelete = window.confirm('Czy jesteś pewien, że chcesz usunąć konto?');
        if (!confirmDelete) return;

        try {
            const response = await axios.delete('https://localhost:7157/api/Auth/delete-account', {
                headers: { Authorization: `Bearer ${token}` },
            });
            alert(response.data);
            localStorage.removeItem('authToken');
            navigate('/navbar');
        } catch (error) {
            setError(error.response?.data?.message || 'Error deleting account.');
            console.error(error);
        }
    };

    useEffect(() => {
        const fetchUserProfile = async () => {
            setLoading(true);
            setError(null);

            const token = localStorage.getItem('authToken');
            if (!token) {
                setError('Authorization token not found.');
                setLoading(false);
                return;
            }

            try {
                const response = await axios.get('https://localhost:7157/api/auth/profile', {
                    headers: { Authorization: `Bearer ${token}` },
                });
                setProfile(response.data);
            } catch (error) {
                if (error.response?.status === 401) {
                    setError('Token is invalid or expired. Please log in again.');
                } else {
                    setError('Error fetching profile data.');
                }
                console.error(error);
            } finally {
                setLoading(false);
            }
        };

        fetchUserProfile();
    }, []);

    useEffect(() => {
        const token = localStorage.getItem('authToken');
        if (!token) {
            navigate('/navbar');
        }
    }, [navigate]);

    if (!profile && !loading) {
        navigate('/navbar');
    }

    return (
        <div className="profile-container">
            {loading ? (
                <p>Ładowanie danych użytkownika...</p>
            ) : error ? (
                <p style={{ color: 'red' }}>{error}</p>
            ) : profile ? (
                <div>
                    <p className="title">Profil użytkownika</p>
                    <p>Użytkownik: {profile.username}</p>
                    <p>Email: {profile.email}</p>
                    <p>Numer telefonu: {profile.phoneNumber || 'Brak danych'}</p>
                </div>
            ) : (
                <p>Nie znaleziono danych użytkownika.</p>
            )}

            {profile && (
                <>
                    <button onClick={handleLogout}>Wyloguj</button>
                    <button onClick={() => setShowChangePasswordForm(!showChangePasswordForm)}>
                        {showChangePasswordForm ? 'Anuluj' : 'Zmień hasło'}
                    </button>

                    {showChangePasswordForm && (
                        <form onSubmit={handlePasswordChange} className="change-password-form">
                            <div>
                                <label htmlFor="currentPassword">Obecne hasło:</label>
                                <input
                                    type="password"
                                    id="currentPassword"
                                    value={currentPassword}
                                    onChange={(e) => setCurrentPassword(e.target.value)}
                                    required
                                />
                            </div>
                            <div>
                                <label htmlFor="newPassword">Nowe hasło:</label>
                                <input
                                    type="password"
                                    id="newPassword"
                                    value={newPassword}
                                    onChange={(e) => setNewPassword(e.target.value)}
                                    required
                                />
                            </div>
                            <button type="submit">Zmień hasło</button>
                            {changePasswordMessage && (
                                <p style={{ color: changePasswordSuccess ? 'green' : 'red' }}>
                                    {changePasswordMessage}
                                </p>
                            )}
                        </form>
                    )}

                    <button onClick={handleDeleteAccount} className="delete-account-button">
                        Usuń konto
                    </button>
                </>
            )}
        </div>
    );
};

export default UserProfile;
