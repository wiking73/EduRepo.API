import React, { useState } from 'react';
import './Styles/Login.css';
import axios from 'axios';
import { jwtDecode } from 'jwt-decode';

import { useNavigate } from 'react-router-dom';

const token = localStorage.getItem('authToken');

const Logowanie = () => {
    const [showPassword, setShowPassword] = useState(false);
    const [loginInfo, setLoginInfo] = useState({
        email: '',
        password: ''
    });
    const [registerInfo, setRegisterInfo] = useState({
        username: '',
        phoneNumber: '',
        email: '',
        password: ''
    });
    const [error, setErrorMessage] = useState(null);
    const [success, setSuccessMessage] = useState(null);
    const [loading, setLoading] = useState(false);
    const [isLogin, setIsLogin] = useState(true);

    const navigate = useNavigate();

    const togglePassword = () => {
        setShowPassword(!showPassword);
    };

    const handleLoginInputChange = (e) => {
        const { name, value } = e.target;
        setLoginInfo((prev) => ({
            ...prev,
            [name]: value
        }));
    };

    const handleRegisterInputChange = (e) => {
        const { name, value } = e.target;
        setRegisterInfo((prev) => ({
            ...prev,
            [name]: value
        }));
    };

    const handleLogin = async (e) => {
        e.preventDefault();
        setErrorMessage(null);
        try {
            setLoading(true);
            const response = await axios.post(
                'https://localhost:7157/api/Auth/login',
                loginInfo
            );

            if (response.status === 200) {
                const { token, username } = response.data;
                console.log('Zalogowano:', { token, username });

                localStorage.setItem('authToken', token);
                localStorage.setItem('displayName', username);

                const decoded = jwtDecode(token);
                localStorage.setItem('role', decoded.role);

                navigate("/dashboard");
            }
        } catch (error) {
            console.error('Błąd logowania:', error);
            setErrorMessage('Błąd logowania, spróbuj ponownie.');
        } finally {
            setLoading(false);
        }
    };

    const handleRegister = async (e) => {
        e.preventDefault();
        setErrorMessage(null);
        try {
            setLoading(true);
            const response = await axios.post(
                'https://localhost:7157/api/Auth/register',
                registerInfo
            );

            if (response.status === 200) {
                const { token, username } = response.data;
                console.log('Zarejestrowano:', { token, username });

                localStorage.setItem('authToken', token);
                localStorage.setItem('username', username);

                const decoded = jwtDecode(token);
                localStorage.setItem('role', decoded.role);
                navigate("/dashboard");
            }
        } catch (error) {
            console.error('Błąd rejestracji:', error);
            setErrorMessage('Błąd rejestracji, spróbuj ponownie.');
        } finally {
            setLoading(false);
        }
    };

    return (
        <div className="login-page">
            {token ? (
                <button
                    type="button"
                    className="login-register-button2"
                    onClick={(e) => {
                        e.preventDefault();
                        localStorage.removeItem('authToken');
                        localStorage.removeItem('displayName');
                        localStorage.removeItem('role');
                        window.location.reload();
                    }}
                >
                    Wyloguj się
                </button>
            ) : (
                <form
                    onSubmit={isLogin ? handleLogin : handleRegister}
                    className="login-form"
                >
                    <p className="login-header">{isLogin ? 'Logowanie' : 'Rejestracja'}</p>
                    <div className="button-container">
                        <button
                            type="button"
                            className="login-register-button1"
                            onClick={(e) => {
                                e.preventDefault();
                                setIsLogin(true);
                                setSuccessMessage(null);
                                setErrorMessage(null);
                            }}
                        >
                            Zaloguj się
                        </button>
                        <button
                            type="button"
                            className="login-register-button2"
                            onClick={(e) => {
                                e.preventDefault();
                                setIsLogin(false);
                                setSuccessMessage(null);
                                setErrorMessage(null);
                            }}
                        >
                            Zarejestruj się
                        </button>
                    </div>

                    {isLogin ? (
                        <input
                            type="email"
                            id="email"
                            name="email"
                            className="email"
                            placeholder="Email"
                            value={loginInfo.email}
                            onChange={handleLoginInputChange}
                            required
                        />
                    ) : (
                        <>
                            <input
                                type="text"
                                id="username"
                                name="username"
                                className="username"
                                placeholder="Nazwa użytkownika"
                                value={registerInfo.username}
                                onChange={handleRegisterInputChange}
                                required
                            />
                            <input
                                type="email"
                                id="email"
                                name="email"
                                className="email"
                                placeholder="Email"
                                value={registerInfo.email}
                                onChange={handleRegisterInputChange}
                                required
                            />
                        </>
                    )}

                    <input
                        type={showPassword ? 'text' : 'password'}
                        id="password"
                        className="haslo"
                        name="password"
                        placeholder="Hasło"
                        value={isLogin ? loginInfo.password : registerInfo.password}
                        onChange={isLogin ? handleLoginInputChange : handleRegisterInputChange}
                        required
                    />

                    <label className="pokaz">
                        <input
                            type="checkbox"
                            checked={showPassword}
                            onChange={togglePassword}
                        />
                        Pokaż hasło
                    </label>

                    <input
                        type="submit"
                        className="zapisz"
                        value={isLogin ? 'Zaloguj się' : 'Zarejestruj'}
                        disabled={loading}
                    />

                    {error && <p style={{ color: 'red' }}>{error}</p>}
                    {success && <p style={{ color: 'green' }}>{success}</p>}
                </form>
            )}
        </div>
    );
};

export default Logowanie;
