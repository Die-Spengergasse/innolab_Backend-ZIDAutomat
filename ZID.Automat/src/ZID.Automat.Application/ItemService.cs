﻿using ZID.Automat.Domain.Models;
using ZID.Automat.Dto;
using ZID.Automat.Repository;
using System.Linq;
using ZID.Automat.Dto.Models;
using AutoMapper;
using ZID.Automat.Exceptions;

namespace ZID.Automat.Application
{
    public class ItemService: IItemService
    {
        private readonly IRepositoryRead _repositoryRead;
        private readonly IRepositoryWrite _repositoryWrite;
        private readonly IMapper _mapper;

        public ItemService(IRepositoryRead repositoryRead, IRepositoryWrite repositoryWrite, IMapper mapper)
        {
            _repositoryRead = repositoryRead;
            _repositoryWrite = repositoryWrite;
            _mapper = mapper;
        }

        public IEnumerable<ItemDisplayDto> AllDisplayItems()
        {
            IEnumerable<Item> Items = _repositoryRead.GetAll<Item>();
            return _mapper.Map<IEnumerable<Item>, IEnumerable<ItemDisplayDto>>(Items);
        }

        public IEnumerable<ItemDisplayDto> PrevBorrowedDisplayItemsUser(string UserName)
        {
            IEnumerable<Borrow> Borrows = _repositoryRead.GetAll<Borrow>();
            IEnumerable<Item> items = Borrows.Where(b => b.User.Name == UserName).Select(b => b?.ItemInstance.Item);
            return _mapper.Map<IEnumerable<ItemDisplayDto>>(items);
        }

        public ItemDetailedDto DetailedItem(int ItemId)
        {
            var item = _repositoryRead.FindById<Item>(ItemId);
            return _mapper.Map<ItemDetailedDto>(item);
        }
        public ItemDetailedDto DetailedItem(Guid QrCode)
        {
            var item = (_repositoryRead.GetAll<Borrow>().Where(b => b.GUID == QrCode).SingleOrDefault() ?? throw new QrCodeNotExistingException()).ItemInstance.Item;
            return _mapper.Map<ItemDetailedDto>(item);
        }
    }

    public interface IItemService
    {
        public IEnumerable<ItemDisplayDto> AllDisplayItems();
        public IEnumerable<ItemDisplayDto> PrevBorrowedDisplayItemsUser(string UserName);
        public ItemDetailedDto DetailedItem(int ItemId);
        public ItemDetailedDto DetailedItem(Guid QrCode);
    }
}
