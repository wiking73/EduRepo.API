import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { useParams, Link } from 'react-router-dom';
import { List, Button } from 'semantic-ui-react';

function UczestnicyKursu() {
    const { id } = useParams();
    const [uczestnicy, setUczestnicy] = useState([]);
    const [error, setError] = useState(null);

    const fetchUczestnicy = async () => {
        try {
            const token = localStorage.getItem('authToken');
            const response = await axios.get(
                `https://localhost:7157/api/Kurs/${id}/uczestnicy`,
                {
                    headers: {
                        Authorization: `Bearer ${token}`
                    }
                }
            );
            setUczestnicy(response.data);
        } catch (err) {
            setError("Błąd podczas pobierania uczestników.");
        }
    };

    const handleRemove = async (uczId) => {
        try {
            const token = localStorage.getItem('authToken');
            if (window.confirm("Czy na pewno chcesz usunąć tego uczestnika?")) {
                await axios.delete(
                    `https://localhost:7157/api/Kurs/${id}/usunuczestnika/${uczId}`,
                    {
                        headers: {
                            Authorization: `Bearer ${token}`
                        }
                    }
                );
                fetchUczestnicy();
            }
        } catch (err) {
            console.error("Błąd przy usuwaniu uczestnika:", err);
            setError("Nie udało się usunąć uczestnika.");
        }
    };

    useEffect(() => {
        fetchUczestnicy();
    }, [id]);

    return (
        <div className="kurs-details">
            <h3>Zaakceptowani uczestnicy kursu</h3>

            {error && <p style={{ color: 'red' }}>{error}</p>}

            {uczestnicy.length === 0 ? (
                <p>Brak uczestników w tym kursie.</p>
            ) : (
                <List divided>
                    {uczestnicy.map((u) => (
                        <List.Item key={u.idUczestnictwa}>
                            <List.Content
                                style={{
                                    display: 'flex',
                                    justifyContent: 'space-between',
                                    alignItems: 'center',
                                    padding: '0.5rem 0'
                                }}
                            >
                                <span>{u.userName}</span>
                                <Button
                                    color="red"
                                    size="mini"
                                    onClick={() => handleRemove(u.idUczestnictwa)}
                                    style={{ marginLeft: '1rem', width: '80px' }}
                                >
                                    Usuń
                                </Button>
                            </List.Content>
                        </List.Item>
                    ))}
                </List>
            )}

            <Link
                to={`/details/${id}`}
                className="btn btn-secondary"
                style={{ marginTop: '1rem', display: 'inline-block' }}
            >
                Powrót do kursu
            </Link>
        </div>
    );
}

export default UczestnicyKursu;
