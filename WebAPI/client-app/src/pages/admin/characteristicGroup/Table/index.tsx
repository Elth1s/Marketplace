import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableContainer from '@mui/material/TableContainer';
import TableHead from '@mui/material/TableHead';
import TablePagination from '@mui/material/TablePagination';
import TableRow from '@mui/material/TableRow';

import Box from '@mui/material/Box';
import Container from '@mui/material/Container';
import Paper from '@mui/material/Paper';

import Typography from '@mui/material/Typography';
import Button from '@mui/material/Button';

import IconButton from '@mui/material/IconButton';
import DeleteIcon from '@mui/icons-material/Delete';
import EditIcon from '@mui/icons-material/Edit';


import { useEffect, useState } from "react";

import { useActions } from "../../../../hooks/useActions";
import { useTypedSelector } from "../../../../hooks/useTypedSelector";

import { baseURL } from '../../../../http_comon';

const CharacteristicGroupTable = () => {
    const { GetCharacteristicGroups, DeleteCharacteristicGroup } = useActions();
    const [loading, setLoading] = useState<boolean>(false);

    const [page, setPage] = useState(0);
    const [rowsPerPage, setRowsPerPage] = useState(5);

    const { characteristicGroups } = useTypedSelector((store) => store.characteristicGroup);

    useEffect(() => {
        getData();
    }, []);

    const getData = async () => {
        setLoading(true);
        try {
            document.title = "Characteristic Group";
            await GetCharacteristicGroups();
            setLoading(false);
        } catch (ex) {
            setLoading(false);
        }
    };

    const onDelete = async (id: number) => {
        await DeleteCharacteristicGroup(id);
        getData();
    }

    const handleChangePage = (event: unknown, newPage: number) => {
        setPage(newPage);
    };

    const handleChangeRowsPerPage = (event: React.ChangeEvent<HTMLInputElement>) => {
        setRowsPerPage(parseInt(event.target.value, 10));
        setPage(0);
    };

    return (
        <>
            <Container
                disableGutters
                maxWidth="lg"
                component="main"
                sx={{ pt: 8, pb: 6 }}>
                <Button
                    variant="contained"
                    href="/characteristicGroup/create"
                    sx={{
                        my: 2,
                        px: 4,
                    }}>
                    Create
                </Button>
                <Paper>
                    <TableContainer>
                        <Table
                            sx={{ minWidth: 650 }}
                            aria-label="simple table">
                            <TableHead>
                                <TableRow>
                                    <TableCell>Id</TableCell>
                                    <TableCell>Name</TableCell>
                                    <TableCell>Action</TableCell>
                                </TableRow>
                            </TableHead>
                            <TableBody>
                                {characteristicGroups && characteristicGroups.slice(page * rowsPerPage, page * rowsPerPage + rowsPerPage).map((row) =>
                                (
                                    <TableRow key={row.id}>
                                        <TableCell component="th" scope="row">
                                            {row.id}
                                        </TableCell>
                                        <TableCell>
                                            {row.name}
                                        </TableCell>
                                        <TableCell>
                                            <IconButton
                                                aria-label="edit"
                                                href={"/characteristicGroup/update?id=" + row.id}>
                                                <EditIcon />
                                            </IconButton>
                                            <IconButton
                                                aria-label="delete"
                                                onClick={() => onDelete(row.id)}>
                                                <DeleteIcon />
                                            </IconButton>
                                        </TableCell>
                                    </TableRow>
                                ))}
                            </TableBody>
                        </Table>
                    </TableContainer>
                    <TablePagination
                        rowsPerPageOptions={[5, 10, 25]}
                        component="div"
                        count={characteristicGroups.length}
                        rowsPerPage={rowsPerPage}
                        page={page}
                        onPageChange={handleChangePage}
                        onRowsPerPageChange={handleChangeRowsPerPage}
                    />
                </Paper>
            </Container>
        </>
    );
}

export default CharacteristicGroupTable