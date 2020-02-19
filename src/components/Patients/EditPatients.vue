<template>
  <div>
    <table class="all">
      <thead>
        <tr>
          <th>Фамилия</th>
          <th>Имя</th>
          <th>Отчество</th>
          <th>Дата рождения</th>
          <th>Пол</th>
          <th>СНИЛС</th>
        </tr>
      </thead>
      <tbody>
        <tr
          class="patient-row row"
          v-for="(info, id) in this.inform" :key="id"
          v-on:click="selectPatient(id, info.patientId)"
          v-bind:class="{selected: selectedId === id}"
        >
            <td class="all-info-item">{{ info.firstName }}</td>
            <td class="all-info-item">{{ info.lastName }}</td>
            <td class="all-info-item">{{ info.middleName }}</td>
            <td class="all-info-item">{{ convertDate(info.birthDate) }}</td>
            <td class="all-info-item">{{ info.gender }}</td>
            <td class="all-info-item">{{ info.snils }}</td>
        </tr>
      </tbody>
    </table>

    <div class="input-section" v-if="isSelected">
      <input
        class="input-field input-field__name"
        type="text"
        placeholder="Введите фамилию"
        v-model="postBody.firstName"
      />
      <input
        class="input-field input-field__name"
        type="text"
        placeholder="Введите имя"
        v-model="postBody.lastName"
      />
      <input
        class="input-field input-field__name"
        type="text"
        placeholder="Введите отчество"
        v-model="postBody.middleName"
      />
      <input
        class="input-field input-field__name"
        type="text"
        placeholder="Введите СНИЛС"
        v-model="postBody.SNILS"
      />
    </div>
    <section class="select" v-if="isSelected">
      <div class="date-birthday">
        <p>Дата рождения</p>
        <input type="date" v-model="postBody.birthDate">
        <img src="../icons/calendar-im.png" alt="календарь">
      </div>
      <div class="gender">
        <p class="gender">Выберите пол</p>
        <input type="radio" id="one" value="Мужской" v-model="postBody.gender">
        <label for="one">Мужской</label>
        <input type="radio" id="two" value="Женский" v-model="postBody.gender">
        <label for="two">Женский</label>
      </div>
    </section>
    <button
      type="button"
      class="button-submit"
      v-if="isSelected"
      v-on:click="updateData()"
    >
      Обновить
    </button>
    <p class="not-selected-text" v-else>Выберите пациента, которому нужно изменить данные</p>
  </div>
</template>

<script>
  import axios from "axios";
  import moment from 'moment';

  export default {
    name: "EditPatients",
    data() {
      return {
        inform: null,
        isSelected: false,
        selectedId: null,
        postBody: {
          "patientId": null,
          "firstName": null,
          "lastName": null,
          "middleName": null,
          "birthDate": null,
          "gender": null,
          "SNILS": null
        },
      }
    },
    methods: {
      convertDate(date) {
        return moment(date).format('DD.MM.YYYY');
      },
      selectDate(date) {
        return moment(date).format('YYYY-MM-DD');
      },
      selectPatient(id, patientId) {
        this.selectedId = id;

        this.isSelected = true;
        this.postBody = {
          "patientId": patientId,
          "firstName": this.inform[id].firstName,
          "lastName": this.inform[id].lastName,
          "middleName": this.inform[id].middleName,
          "birthDate": this.selectDate(this.inform[id].birthDate),
          "gender": this.inform[id].gender,
          "SNILS": this.inform[id].snils
        }
      },
      updateData() {
        axios.put(`https://localhost:5001/api/patient/${this.postBody.patientId}`, this.postBody)
          .then((response) => {
            console.log(response);
            axios
              .get('https://localhost:5001/api/patient')
              .then(response => (this.inform = response.data));
            this.isSelected = false;
            this.selectedId = null;
          })
          .catch((error) => {
            console.log(error);
          });
      }
    },
    mounted() {
      axios
        .get('https://localhost:5001/api/patient')
        .then(response => (this.inform = response.data));
    },
  }
</script>

<style lang="scss">
  .all {
    margin-bottom: 15px;
  }
  
  .patient-row {
    cursor: pointer;
  }

  .not-selected-text {
    margin: 50px 0 0;
    font-size: 24px;
  }

  .row.selected {
    background-color: #ed462f;
  }
</style>