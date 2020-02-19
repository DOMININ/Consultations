<template>
  <div>
    <div class="title-block">
      <p class="title-element">Фамилия</p>
      <p class="title-element">Имя</p>
      <p class="title-element">Отчество</p>
      <p class="title-element">СНИЛС</p>
    </div>
    <ul class="inform-list">
      <li
        class="inform-item"
        v-for="(info, idP) in this.inform"
        :key="idP"
        v-on:click="selectPatient(idP)"
      >
        <div class="inform-block"
             v-bind:class="{selected: selectedId === idP}">
          <p class="inform-element">{{ info.firstName }}</p>
          <p class="inform-element">{{ info.lastName }}</p>
          <p class="inform-element">{{ info.middleName }}</p>
          <p class="inform-element">{{ info.snils }}</p>
        </div>
        <ul
          v-if="selectedId === idP"
          v-for="(cons, idC) in info.consultations"
          :key="idC"
          class="cons-list"
        >
          <li class="cons-item">{{ idC + 1 }}</li>
          <li class="cons-item">Дата: {{ convertDate(cons.date) }}</li>
          <li class="cons-item">Время: {{ convertTime(cons.time) }}</li>
          <li class="cons-item">Симптомы: <br> {{ cons.symptoms }}</li>
        </ul>
        <p
          v-if="selectedId === idP && Object.keys(info.consultations).length === 0"
          class="cons-empty"
        >У данного пациента нет консультаций</p>
      </li>
    </ul>
    <p v-if="!isSelected" class="not-selected-text">Выберите пациента для просмотра консультаций</p>
  </div>
</template>

<script>
  import axios from "axios";
  import moment from "moment";

  export default {
    name: "ConsultPatients",
    data() {
      return {
        inform: null,
        selectedId: null,
        isSelected: false,
      }
    },
    methods: {
      selectPatient(idP) {
        this.selectedId = idP;
        this.isSelected = true;
      },
      convertDate(date) {
        return moment(date).format('DD.MM.YYYY');
      },
      convertTime(time) {
        return moment(time).format('HH:mm');
      },
    },
    mounted() {
      axios
        .get('https://localhost:5001/api/consultation')
        .then(response => (this.inform = response.data));
    },
  }
</script>

<style lang="scss">
  .inform-list {
    list-style: none;
    margin: 0;
    padding: 0;
  }

  .inform-item {
    &:nth-child(odd) {
      background-color: #e5e5e5;
    }

    &:nth-child(even) {
      background-color: #f2f2f2;
    }

    &:not(:last-child) {
      border-bottom: 1px solid #c2c2c2;
    }
  }

  .inform-block {
    display: flex;
    justify-content: space-around;
    cursor: pointer;

    &:hover {
      background: #66d1c6;
      transition-duration: 0.5s;
    }
  }

  .inform-element {
    display: inline-block;
    margin: 0;
    padding: 10px;
    min-width: 140px;
    text-align: center;
  }

  .title-block {
    display: flex;
    justify-content: space-around;
    background-color: #66d1c6;
    color: #fff;
  }

  .title-element {
    display: inline-block;
    margin: 0;
    padding: 10px;
    min-width: 140px;
    text-align: center;
  }

  .cons-list {
    display: flex;
    justify-content: space-around;
    list-style: none;
    margin: 0;
    padding: 0;
    background-color: #fff;
  }

  .cons-item {
    padding: 7px;
    min-width: 140px;
    word-wrap: break-word;
    text-align: center;
  }

  .cons-empty {
    margin: 0;
    padding: 10px;
    font-size: 18px;
    display: flex;
    justify-content: center;
    background-color: #cccccc;
  }

  .selected {
    background-color: #ed462f;

    &:hover {
      background-color: #ed462f;
    }
  }
</style>