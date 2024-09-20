import Button from 'react-bootstrap/Button';
// import Modal from 'react-bootstrap/Modal';
// import { useDispatch ,useSelector} from 'react-redux';
// import { DeleteProduct, getProducts } from '../../../redux/product/ProductAction';
// import { toast } from 'react-toastify';
// function ModalDeleteProduct(props){
// const{dataDelete,show,handleShow,handleClose,HandleGetAllProduct}=props;
// const displaydata=`${dataDelete.name}`
// const dispatch = useDispatch();

// const handleDelete= async()=>{
//     dispatch(DeleteProduct(dataDelete.productId));
//     toast.success("Xóa Vật Phẩm Thành Công !!!")
//     handleClose();
    
// }
// return (
//     <>
//       <Modal show={show} onHide={handleClose}>
//         <Modal.Header closeButton>
//           <Modal.Title>Xóa Sản Phẩm</Modal.Title>
//         </Modal.Header>
//         <Modal.Body>
//   Bạn có chắc muốn xóa sản phẩm <strong>{displaydata}</strong> ?
// </Modal.Body>
//         <Modal.Footer>
//         <Button variant="primary" onClick={()=>handleDelete()}>
//             Có
//           </Button>
//           <Button variant="secondary" onClick={()=>handleClose()}>
//             Không
//           </Button>
          
//         </Modal.Footer>
//       </Modal>
//     </>
//   );
// }



// export default ModalDeleteProduct;