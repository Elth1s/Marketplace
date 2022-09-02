import { Box, Typography, TableRow, Checkbox } from '@mui/material';

import { useEffect, useState } from 'react';
import { useTranslation } from 'react-i18next';

import { useActions } from '../../../../hooks/useActions';
import { useTypedSelector } from '../../../../hooks/useTypedSelector';

import EnhancedTable from '../../../../components/EnhancedTable';
import { TableCellStyle } from '../../../../components/EnhancedTable/styled';

import Create from '../Create';
import Update from '../Update';

import { ICharacteristicGroupInfo } from '../types';
import { HeadCell } from '../../../../store/types';


const CharacteristicGroupTable = () => {
    const { t } = useTranslation();

    const headCells: HeadCell<ICharacteristicGroupInfo>[] = [
        {
            id: 'id',
            numeric: true,
            label: `${t('containers.admin_seller.tableHeadCell.identifier')}`,
        },
        {
            id: 'name',
            numeric: false,
            label: `${t('containers.admin_seller.tableHeadCell.name')}`,
        },
    ];

    const [page, setPage] = useState(1);
    const [rowsPerPage, setRowsPerPage] = useState(8);
    const [name, setName] = useState("");
    const [isAscOrder, setIsAscOrder] = useState<boolean>(true);
    const [orderBy, setOrderBy] = useState<keyof ICharacteristicGroupInfo>('id');

    const [selected, setSelected] = useState<readonly number[]>([]);

    const { SearchCharacteristicGroups, DeleteCharacteristicGroups } = useActions();
    const { characteristicGroups, count } = useTypedSelector((store) => store.characteristicGroup);

    useEffect(() => {
        document.title = `${t('containers.admin_seller.sideBar.characteristicGroups')}`;
        getData();
    }, [page, rowsPerPage, name, isAscOrder, orderBy]);

    const getData = async () => {
        try {
            await SearchCharacteristicGroups(page, rowsPerPage, name, isAscOrder, orderBy, true);
            setSelected([]);
        } catch (ex) {
        }
    };

    const onDelete = async () => {
        await DeleteCharacteristicGroups(selected);
        if (page == 1)
            getData()
        else
            await setPage(1);
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
            const newSelecteds = characteristicGroups.map((n) => n.id);
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
            <Box sx={{ display: "flex", justifyContent: "space-between", alignItems: "center", my: "30px" }}>
                <Typography variant="h1">{t('containers.admin_seller.sideBar.characteristicGroups')}</Typography>
                <Create afterCreate={() => { getData() }} />
            </Box>
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
                update={<Update id={selected[selected.length - 1]} afterUpdate={() => { getData() }} />}
                tableBody={
                    characteristicGroups.map((row, index) => {
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
                                <TableCellStyle align="center">{row.name}</TableCellStyle>
                            </TableRow>
                        );
                    })
                }
            />
        </>
    );
}

export default CharacteristicGroupTable