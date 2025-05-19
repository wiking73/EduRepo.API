import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { Link, useParams } from 'react-router-dom';
import './Styles/kursy.css';

const Odpowiedzi = () => {
    const [odpowiedzi, setOdpowiedzi] = useState([]);
    const [error, setError] = useState(null);
    const [loading, setLoading] = useState(true);

    const token = localStorage.getItem('authToken');
    const { id, IdZadania } = useParams();

    useEffect(() => {
        fetchOdpowiedzi();
    }, []);

    const fetchOdpowiedzi = async () => {
        setLoading(true);
        try {
            const response = await axios.get('https://localhost:7157/api/Odpowiedz');
            setOdpowiedzi(response.data);
        } catch (err) {
            console.error('Błąd pobierania listy:', err);
            setError('Nie udało się pobrać listy.');
        } finally {
            setLoading(false);
        }
    };

    const handleDeleteOdpowiedz = async (idOdpowiedzi) => {
        try {
            if (!token) {
                setError('Musisz być zalogowany!');
                return;
            }

            if (window.confirm('Czy na pewno chcesz usunąć tę odpowiedź?')) {
                await axios.delete(`https://localhost:7157/api/Odpowiedz/${idOdpowiedzi}`, {
                    headers: { Authorization: `Bearer ${token}` },
                });
                setOdpowiedzi(prev => prev.filter(b => b.idOdpowiedzi !== idOdpowiedzi));
            }
        } catch (err) {
            console.error('Błąd usuwania:', err);
        }
    };

    const handleEditOdpowiedz = (idOdpowiedzi) => {
        console.log(`Edytuj odpowiedź o ID: ${idOdpowiedzi} (tylko Admin)`);
    };

    if (loading) return <p>Ładowanie danych...</p>;
    if (error) return <p style={{ color: 'red' }}>{error}</p>;

    const odpowiedziDlaZadania = odpowiedzi.filter(odpowiedz => odpowiedz.idZadania === parseInt(IdZadania, 10));

    return (
        <div className="bike-list-background">
            <div className="bike-list-container">
                {odpowiedziDlaZadania.length > 0 ? (
                    <div className="bike-grid">
                        {odpowiedziDlaZadania.map((odpowiedz) => (
                            <div className="bike-container" key={odpowiedz.idOdpowiedzi}>
                                <p>{odpowiedz.nazwaPliku}</p>
                                <div>
                                    <Link to={`/details/${odpowiedz.idOdpowiedzi}`} className="bike-item-button">
                                        Szczegóły
                                    </Link>
                                    <Link to={`/edit/${id}/${IdZadania}/${odpowiedz.idOdpowiedzi}`} className="edit-button">
                                        Oceń
                                    </Link>
                                    <button
                                        onClick={() => handleDeleteOdpowiedz(odpowiedz.idOdpowiedzi)}
                                        className="bike-item-button2"
                                    >
                                        Usuń
                                    </button>
                                </div>
                            </div>
                        ))}
                    </div>
                ) : (
                    <p>Brak odpowiedzi do wyświetlenia.</p>
                )}
            </div>
        </div>
    );
};

export default Odpowiedzi;
