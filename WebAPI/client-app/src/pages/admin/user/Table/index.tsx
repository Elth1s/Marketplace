import { Avatar, Checkbox, TableRow } from '@mui/material';
import { useEffect, useState } from 'react';
import { useActions } from '../../../../hooks/useActions';
import { useTypedSelector } from '../../../../hooks/useTypedSelector';

import { IUserInfo } from '../types';
import { HeadCell } from '../../../../store/types';

import EnhancedTable from '../../../../components/EnhancedTable';
import { TableCellStyle } from '../../../../components/EnhancedTable/styled';


const headCells: HeadCell<IUserInfo>[] = [
    {
        id: 'id',
        numeric: true,
        label: 'Identifier',
    },
    {
        id: 'firstName',
        numeric: false,
        label: 'First Name',
    },
    {
        id: 'secondName',
        numeric: false,
        label: 'Second Name',
    },
    {
        id: 'photo',
        numeric: false,
        label: 'Photo',
    },
    {
        id: 'email',
        numeric: false,
        label: 'Email',
    },
    {
        id: 'phone',
        numeric: false,
        label: 'Phone',
    }
];

const UserTable = () => {
    const [page, setPage] = useState(1);
    const [rowsPerPage, setRowsPerPage] = useState(8);
    const [name, setName] = useState("");
    const [isAscOrder, setIsAscOrder] = useState<boolean>(true);
    const [orderBy, setOrderBy] = useState<keyof IUserInfo>('id');

    const [selected, setSelected] = useState<readonly number[]>([]);

    const { SearchUsers, DeleteUsers } = useActions();
    const { users, count } = useTypedSelector((store) => store.user);

    useEffect(() => {
        document.title = "Users";
        getData();
    }, [page, rowsPerPage, name, isAscOrder, orderBy]);

    const getData = async () => {
        try {
            await SearchUsers(page, rowsPerPage, name, isAscOrder, orderBy);
            setSelected([]);
        } catch (ex) {
        }
    };

    const onDelete = async () => {
        await DeleteUsers(selected);
        setPage(1);
        getData();
    }

    const handleClick = (event: React.MouseEvent<unknown>, id: number) => {
        const selectedIndex = selected.indexOf(id);
        let newSelected: readonly number[] = [];

        if (selectedIndex === -1) {
            newSelected = newSelected.concat(selected, id);
        } else if (selectedIndex === 0) {
            newSelected = newSelected.concat(selected.slice(1));
        } else if (selectedIndex === selected.length - 1) {
            newSelected = newSelected.concat(selected.slice(0, -1));
        } else if (selectedIndex > 0) {
            newSelected = newSelected.concat(
                selected.slice(0, selectedIndex),
                selected.slice(selectedIndex + 1),
            );
        }

        setSelected(newSelected);
    };

    const handleSelectAllClick = (event: React.ChangeEvent<HTMLInputElement>) => {
        if (selected.length == 0) {
            const newSelecteds = users.map((n) => n.id);
            setSelected(newSelecteds);
            return;
        }
        setSelected([]);
    };

    const isSelected = (id: number) => selected.indexOf(id) !== -1;

    const handleChangePage = (event: unknown, newPage: number) => {
        setPage(newPage);
    };

    const handleChangeRowsPerPage = (event: React.ChangeEvent<HTMLInputElement>) => {
        setRowsPerPage(parseInt(event.target.value, 10));
        setPage(1);
    };

    return (
        <>

            <EnhancedTable
                page={page}
                rowsPerPage={rowsPerPage}
                handleChangePage={handleChangePage}
                handleChangeRowsPerPage={handleChangeRowsPerPage}
                setName={setName}
                handleSelectAllClick={handleSelectAllClick}
                isAscOrder={isAscOrder}
                setIsAscOrder={setIsAscOrder}
                orderBy={orderBy}
                setOrderBy={setOrderBy}
                headCells={headCells}
                numSelected={selected.length}
                count={count}
                show={null}
                update={null}
                onDelete={onDelete}
                tableBody={users.map((row, index) => {
                    const isItemSelected = isSelected(row.id);
                    const labelId = `enhanced-table-checkbox-${index}`;

                    return (
                        <TableRow
                            hover
                            onClick={(event) => handleClick(event, row.id)}
                            role="checkbox"
                            aria-checked={isItemSelected}
                            tabIndex={-1}
                            key={row.id}
                            selected={isItemSelected}
                        >
                            <TableCellStyle sx={{ paddingLeft: "11px" }} padding="checkbox">
                                <Checkbox
                                    color="primary"
                                    sx={{ borderRadius: "12px" }}
                                    checked={isItemSelected}
                                    inputProps={{
                                        'aria-labelledby': labelId,
                                    }} />
                            </TableCellStyle>
                            <TableCellStyle
                                component="th"
                                id={labelId}
                                scope="row"
                            >
                                {row.id}
                            </TableCellStyle>
                            <TableCellStyle align="center">{row.firstName}</TableCellStyle>
                            <TableCellStyle align="center">{row.secondName}</TableCellStyle>

                            <TableCellStyle align="center">
                                <Avatar
                                    sx={{ mx: "auto" }}
                                    src={row.photo}
                                    alt="Photo"
                                /></TableCellStyle>
                            <TableCellStyle align="center">{row.email}</TableCellStyle>
                            <TableCellStyle align="center">{row.phone}</TableCellStyle>
                        </TableRow>
                    );
                })} />
        </>
    );
}

export default UserTable