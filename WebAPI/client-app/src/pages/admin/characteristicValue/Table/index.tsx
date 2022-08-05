import TableRow from '@mui/material/TableRow';
import Checkbox from '@mui/material/Checkbox';

import { useEffect, useState } from "react";

import { useActions } from "../../../../hooks/useActions";
import { useTypedSelector } from "../../../../hooks/useTypedSelector";

import EnhancedTable from '../../../../components/EnhancedTable';
import { TableCellStyle } from '../../../../components/EnhancedTable/styled';

import { HeadCell } from '../../../../store/types';
import { ICharacteristicValueInfo } from '../../../seller/characteristicValue/types';
import { Typography } from '@mui/material';

const headCells: HeadCell<ICharacteristicValueInfo>[] = [
    {
        id: 'id',
        numeric: true,
        label: 'Identifier',
    },
    {
        id: 'value',
        numeric: false,
        label: 'Value',
    },
    {
        id: 'characteristicName',
        numeric: false,
        label: 'Characteristic Name',
    },
];

const AdminCharacteristicValueTable = () => {
    const [page, setPage] = useState(1);
    const [rowsPerPage, setRowsPerPage] = useState(8);
    const [name, setName] = useState("");
    const [isAscOrder, setIsAscOrder] = useState<boolean>(true);
    const [orderBy, setOrderBy] = useState<keyof ICharacteristicValueInfo>('id');

    const [selected, setSelected] = useState<readonly number[]>([]);

    const { SearchCharacteristicValues, DeleteCharacteristicValues } = useActions();
    const { characteristicValues, count } = useTypedSelector((store) => store.characteristicValue);

    useEffect(() => {
        getData();
        document.title = "Characteristic Value";
    }, [page, rowsPerPage, name, isAscOrder, orderBy]);

    const getData = async () => {
        try {
            await SearchCharacteristicValues(page, rowsPerPage, name, isAscOrder, orderBy, false);
            setSelected([]);
        } catch (ex) {
        }
    };

    const onDelete = async () => {
        await DeleteCharacteristicValues(selected);
        setPage(1);
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
            const newSelecteds = characteristicValues.map((n) => n.id);
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
            <Typography variant="h1" sx={{ my: "30px", py: "4.5px" }}>Characteristic values</Typography>
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
                onDelete={onDelete}
                update={null}
                tableBody={
                    characteristicValues.map((row, index) => {
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
                                        }}
                                    />
                                </TableCellStyle>
                                <TableCellStyle
                                    component="th"
                                    id={labelId}
                                    scope="row"
                                >
                                    {row.id}
                                </TableCellStyle>
                                <TableCellStyle align="center">{row.value}</TableCellStyle>
                                <TableCellStyle align="center">{row.characteristicName}</TableCellStyle>
                            </TableRow>
                        );
                    })
                }
            />
        </>
    );
}

export default AdminCharacteristicValueTable;