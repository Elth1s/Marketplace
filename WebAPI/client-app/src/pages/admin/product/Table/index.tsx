import { Avatar, Checkbox, TableRow, Typography } from '@mui/material';
import { useEffect, useState } from 'react';
import { useTranslation } from 'react-i18next';

import { useActions } from '../../../../hooks/useActions';
import { useTypedSelector } from '../../../../hooks/useTypedSelector';

import { HeadCell } from '../../../../store/types';

import EnhancedTable from '../../../../components/EnhancedTable';
import { TableCellStyle } from '../../../../components/EnhancedTable/styled';
import { IProductInfo } from '../../../seller/product/types';

const AdminProductTable = () => {
    const { t } = useTranslation();

    const headCells: HeadCell<IProductInfo>[] = [
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
        {
            id: 'image',
            numeric: false,
            label: `${t('containers.admin_seller.tableHeadCell.image')}`,
        },
        {
            id: 'price',
            numeric: false,
            label: `${t('containers.admin_seller.tableHeadCell.price')}`,
        },
        {
            id: 'count',
            numeric: false,
            label: `${t('containers.admin_seller.tableHeadCell.count')}`,
        },
        {
            id: 'statusName',
            numeric: false,
            label: `${t('containers.admin_seller.tableHeadCell.productStatus')}`,
        },
        {
            id: 'categoryName',
            numeric: false,
            label: `${t('containers.admin_seller.tableHeadCell.category')}`,
        }
    ];

    const [page, setPage] = useState(1);
    const [rowsPerPage, setRowsPerPage] = useState(8);
    const [name, setName] = useState("");
    const [isAscOrder, setIsAscOrder] = useState<boolean>(true);
    const [orderBy, setOrderBy] = useState<keyof IProductInfo>('id');

    const [selected, setSelected] = useState<readonly number[]>([]);

    const { SearchProducts, DeleteProducts } = useActions();
    const { products, count } = useTypedSelector((store) => store.productSeller);

    useEffect(() => {
        document.title = `${t('containers.admin_seller.sideBar.products')}`;
        getData();
    }, [page, rowsPerPage, name, isAscOrder, orderBy]);

    const getData = async () => {
        try {
            await SearchProducts(page, rowsPerPage, name, isAscOrder, orderBy, false);
            setSelected([]);
        } catch (ex) {
        }
    };

    const onDelete = async () => {
        await DeleteProducts(selected);
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
            const newSelecteds = products.map((n) => n.id);
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
            <Typography variant="h1" sx={{ my: "30px", py: "4.5px" }}>{t('containers.admin_seller.sideBar.products')}</Typography>
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
                tableBody={products.map((row, index) => {
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
                            <TableCellStyle align="center">{row.name}</TableCellStyle>
                            <TableCellStyle align="center">
                                <Avatar
                                    sx={{ mx: "auto" }}
                                    src={row.image}
                                />
                            </TableCellStyle>
                            <TableCellStyle align="center">{row.price}</TableCellStyle>
                            <TableCellStyle align="center">{row.count}</TableCellStyle>
                            <TableCellStyle align="center">{row.statusName}</TableCellStyle>
                            <TableCellStyle align="center">{row.categoryName}</TableCellStyle>
                        </TableRow>
                    );
                })} />
        </>
    );
}

export default AdminProductTable