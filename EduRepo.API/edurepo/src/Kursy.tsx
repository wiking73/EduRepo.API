import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { Link, NavLink } from 'react-router-dom';
import './Styles/kursy.css';

import { Menu, Button } from 'semantic-ui-react';

interface Kurs {
    idKursu: number;
    nazwa: string;
    opis: string;
    rokAkademicki: string;
    klasa: string;
    czyZarchiwizowany: true;
}

const Kursy: React.FC = () => {
    const [kursy, setkursy] = useState<Kurs[]>([]);
    const [error, setError] = useState<string | null>(null);
    const [loading, setLoading] = useState<boolean>(true);

    const token = localStorage.getItem('authToken');
    const role = localStorage.getItem('role');

    useEffect(() => {
        fetchKursy();
    }, []);

    const fetchKursy = async () => {
        setLoading(true);
        try {
            const response = await axios.get<Kurs[]>('https://localhost:7157/api/Kurs');
            setkursy(response.data);
        } catch (err: any) {
            console.error('B³¹d pobierania listy kursów:', err);
            setError('Nie uda³o siê pobraæ listy kursów.');
        } finally {
            setLoading(false);
        }
    };

    const handleDeleteKurs = async (idKursu: number) => {
        try {
            if (!token) {
                setError('Musisz byæ zalogowany!');
                return;
            }

            if (window.confirm('Czy na pewno chcesz usun¹æ ten kurs?')) {
                await axios.delete(`https://localhost:7157/api/Kurs/${idKursu}`, {
                    headers: { Authorization: `Bearer ${token}` },
                });
                setkursy((prev) => prev.filter((b) => b.idKursu !== idKursu));
            }
        } catch (err: any) {
            console.error('B³¹d', err);
        }
    };

    const handleAddKurs = () => {
        console.log('Dodaj kurs (tylko Admin)');
    };

    const handleEditKurs = (idkurs: number) => {
        console.log(`Edytuj kurs o ID: ${idkurs} (tylko Admin)`);
    };

    const handleZapiszKurs = async (idKursu: number) => {
        try {
            if (!token) {
                setError('Musisz byæ zalogowany!');
                return;
            }

            // Zapisywanie u¿ytkownika na kurs (post request)
            const userId = localStorage.getItem('userId');
            const response = await axios.post(
                `https://localhost:7157/api/Uczestnictwo/${idKursu}/dolacz`,
                {},
                {
                    headers: { Authorization: `Bearer ${token}` }
                }
            );

            if (response.status === 200) {
                alert('Zosta³eœ zapisany na kurs!');
            }
        } catch (err: any) {
            console.error('B³¹d zapisywania na kurs:', err);
            setError('Nie uda³o siê zapisaæ na kurs.');
        }
    };

    if (loading) {
        return <p>£adowanie danych...</p>;
    }

    if (error) {
        return <p style={{ color: 'red' }}>{error}</p>;
    }

    return (
        <div className="bike-list-background">
            <div className="bike-list-container">
                {/* Menu dla Admina */}
                <div className="bike-list__header">
                    <Menu.Item as={NavLink} to="/kurs/create">
                        <Button content="Dodaj Kurs" size="large" className="custom-button17" onClick={handleAddKurs} />
                    </Menu.Item>
                    <Menu.Item as={NavLink} to="/bikes/filtersort">
                        <Button content="Filtrowanie/Sortowanie" size="large" className="custom-button18" />
                    </Menu.Item>
                </div>

                {kursy.length > 0 ? (
                    <div className="bike-grid">
                        {kursy.map((kurs) => (
                            <div className="bike-container" key={kurs.idKursu}>
                                <h3>{kurs.nazwa}</h3>
                                <div>
                                    <Link to={`/details/${kurs.idKursu}`} className="bike-item-button">
                                        Szczegó³y
                                    </Link>

                                    <Link to={`/edit/${kurs.idKursu}`} className="edit-button">
                                        Edytuj
                                    </Link>
                                    <button
                                        onClick={() => handleDeleteKurs(kurs.idKursu)}
                                        className="bike-item-button2"
                                    >
                                        Usuñ
                                    </button>

                                    {/* Przycisk do zapisywania na kurs */}
                                    <button
                                        onClick={() => handleZapiszKurs(kurs.idKursu)}
                                        className="bike-item-button2"
                                    >
                                        Zapisz Mnie
                                    </button>
                                </div>
                            </div>
                        ))}
                    </div>
                ) : (
                    <p>Brak kursów do wyœwietlenia.</p>
                )}
            </div>
        </div>
    );
};

export default Kursy;
